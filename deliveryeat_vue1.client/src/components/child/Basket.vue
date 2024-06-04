<template>
    <div class="large-6 cell">
        <div class="text-right" >
            <span class="notification" v-if="gettersAuthData.role!=''">
                {{gettersAuthData.role}}
                <router-link v-if="gettersAuthData.role!=''" to="/basketuser" exact><img v-if="gettersAuthData.role!=''" src="./cor.png" alt="" id="basket"> </router-link>




                <span class="badge">{{count}}</span>
            </span>
            <span class="notification" v-if="gettersAuthData.role==''">
                {{gettersAuthData.role}}
                <router-link  to="/basket" exact><img  src="./cor.png" alt="" id="basket"> </router-link>




                <span class="badge">{{count}}</span>
            </span>
            <router-link to="/Login" v-if="!isAuth"><a class="secondary button" id="Sigin">Войти</a></router-link>
            <a v-if="isAuth" class="secondary button" id="Sigin" @click="signout">Выйти</a>

        </div>

    </div>
</template>

<script>

    import emitter from 'tiny-emitter/instance'
    import { ref } from 'vue'
    import router from "@/router.js";

    import axios from "axios";
    const API_URL = "http://localhost:5000/"
    import {mapGetters} from 'vuex';
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
                    isAuth:false,


                }
            },


            methods: {
                async refreshData() {
                    if (getCookie("session") != undefined) {
                        axios.get(API_URL + "api/Basket/api/GetCount?session=" + getCookie("session")).then(
                            (response) => {
                                const data = response.data;
                                this.count = data;


                            }
                        )
                    }

                },
                async authget(){
                    if(this.gettersAuthData.userName==""){
                        this.isAuth=false;
                    }
                    else if(this.gettersAuthData.userName!=""){
                        this.isAuth=true;
                    }
                },
                signout(){
                    localStorage.removeItem('access_token');
                    location.reload();

                }






            },


            created() {
                this.refreshData();
                this.authget();
                this.timer = setInterval(this.refreshData, 10000);
                this.timer = setInterval(this.authget, 1000);
            },
            mounted: function () {
                this.refreshData();
                this.authget();

            },
            computed: {
                ...mapGetters('auth', {
                    gettersAuthData: 'getAuthData',
                    getterLoginStatus:'getLoginStatus'
                })},

        }
        </script>