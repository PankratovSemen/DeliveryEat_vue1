<template>
    <div class="large-6 cell">
        <div class="text-right" >
            <span class="notification">
                <img src="./cor.png" alt="" id="basket">
                <span class="badge">{{count}}</span>
            </span>
            <a href="#" class="secondary button" id="Sigin">Войти</a>

        </div>

    </div>
</template>

<script>

    import emitter from 'tiny-emitter/instance'
    import { ref } from 'vue'
    const API_URL = "https://localhost:7084/"
    function getCookie(name) {
        let matches = document.cookie.match(new RegExp(
            "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
        ));
        return matches ? decodeURIComponent(matches[1]) : undefined;

    }
    function setCookie(name, value, attributes = {}) {

        attributes = {
            path: '/',
            // add other defaults here if necessary
            ...attributes
        };

        if (attributes.expires instanceof Date) {
            attributes.expires = attributes.expires.toUTCString();
        }

        let updatedCookie = encodeURIComponent(name) + "=" + encodeURIComponent(value);

        for (let attributeKey in attributes) {
            updatedCookie += "; " + attributeKey;
            let attributeValue = attributes[attributeKey];
            if (attributeValue !== true) {
                updatedCookie += "=" + attributeValue;
            }
        }

        document.cookie = updatedCookie;
    }
    
    export default
        {
           
            data() {
                return {

                    count:0,
                    


                }
            },


            methods: {
                async refreshData() {
                    axios.get(API_URL + "api/Basket/api/GetCount?session="+getCookie("session")).then(
                        (response) => {
                            const data = response.data;
                            this.count = data;


                        }
                    )

                },







            },

            created() {
                this.refreshData();
                this.timer = setInterval(this.refreshData, 10000);
            },
            mounted: function () {
                this.refreshData();


            },


        }
        </script>