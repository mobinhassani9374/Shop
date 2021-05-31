var app = new Vue({
    el: "#app",
    data: {
        countCart: 0,
        cart: [],
        price: 0,
        loading: false,
        shippingCost: 0,
        category: [],
    },
    methods: {
        addCart: function(productId) {
            var me = this;
            var l = Ladda.create(document.getElementById("addcart" + productId));
            $.ajax({
                    url: "/Cart/Add",
                    method: "post",
                    data: { ProductId: productId },
                })
                .then(function(response) {
                    me.countCart = response.data.length;
                    toastr.info(response.message);
                    l.stop();
                })
                .fail(function(error) {
                    l.stop();
                    if (error.status === 401) {
                        Swal.fire({
                            icon: "error",
                            title: "هشدار",
                            text: "برای افزودن محصول به سبد خریدتان ابتدا باید عضو سایت باشید",
                            confirmButtonText: "عضویت در سایت",
                            footer: '<a href="/Account/Login?returnUrl=addToCart=' +
                                productId +
                                '">اگر قبلا عضو سایت شدید از اینجا وارد شوید</a>',
                        }).then(function(result) {
                            if (result.value) {
                                window.location =
                                    "/Account/Register?returnUrl=addToCart=" + productId;
                            }
                        });
                    }
                });
        },
        getCart: function() {
            var me = this;
            me.loading = true;
            $.ajax({
                    url: "/Cart/Get",
                    method: "post",
                })
                .then(function(response) {
                    me.cart = response;
                    me.countCart = response.length;
                    me.computedPrice();
                    me.loading = false;
                })
                .fail(function() {
                    me.loading = false;
                });
        },
        seperator: function(price) {
            var pattern = /(-?\d+)(\d{3})/;
            while (pattern.test(price))
                price = price.toString().replace(pattern, "$1,$2");
            return price;
        },
        computedPrice: function() {
            this.price = 0;
            var me = this;
            me.price = 0;
            me.cart.map(function(item) {
                if (me.disCount !== 0) {
                    me.price += (item.price * item.count * (100 - item.disCount)) / 100;
                } else {
                    me.price += item.price * item.count;
                }
            });
            // if (me.price < 700000) {
            //     me.shippingCost = 20000;
            //     me.price += 20000;
            // }
            // else {
            //     me.shippingCost = 0;
            // }
            me.shippingCost = 30000;
            me.price += 30000;
        },
        increase: function(productId) {
            var me = this;
            me.loading = true;
            $.ajax({
                url: "/Cart/Increase",
                method: "post",
                data: { ProductId: productId },
            }).then(function(response) {
                if (response.isSuccess) {
                    toastr.info(response.message);
                    me.countCart = response.data.length;
                    me.cart = response.data;
                } else {
                    toastr.error(response.errors[0]);
                }
                me.computedPrice();
                me.loading = false;
            });
        },
        reduce: function(productId) {
            var me = this;
            me.loading = true;
            $.ajax({
                url: "/Cart/Reduce",
                method: "post",
                data: { ProductId: productId },
            }).then(function(response) {
                if (response.isSuccess) {
                    toastr.info(response.message);
                    me.countCart = response.data.length;
                    me.cart = response.data;
                } else {
                    toastr.error(response.errors[0]);
                }
                me.computedPrice();
                me.loading = false;
            });
        },
        deleteCart: function(productId) {
            var me = this;
            if (confirm("آیا مطمئن هستید ؟؟ ")) {
                me.loading = true;
                $.ajax({
                    url: "/Cart/Delete",
                    method: "post",
                    data: { ProductId: productId },
                }).then(function(response) {
                    me.countCart = response.data.length;
                    me.cart = response.data;
                    toastr.info(response.message);
                    me.computedPrice();
                    me.loading = false;
                });
            }
        },
        getCategory: function() {
            var me = this;
            me.loading = true;
            $.ajax({
                url: "/Category/Get",
                method: "post",
            }).then(function(response) {
                me.category = response;
            });
        },
    },
    created: function() {
        this.getCart();
        this.getCategory();
    },
});

Vue.component("item", {
    template: "#item-template",
    props: {
        model: Object,
    },
    data: function() {
        return {
            open: false,
        };
    },
    computed: {
        isFolder: function() {
            return this.model.children && this.model.children.length;
        },
    },
    methods: {
        toggle: function() {
            if (this.isFolder) {
                this.open = !this.open;
            }
        },
    },
});

Vue.component("category", {
    template: "#category-template",
    props: {
        model: Object,
    },
    data: function() {
        return {
            open: false,
        };
    },
    computed: {
        isFolder: function() {
            return this.model.children && this.model.children.length;
        },
    },
    methods: {
        toggle: function() {
            if (this.isFolder) {
                this.open = !this.open;
            }
        },
    },
});