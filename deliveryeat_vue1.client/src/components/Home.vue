<script setup>
    /**
  * теперь этот файл/поток будет кодироваться в UTF-8
  */
    import { ref } from 'vue'
    import Counter from './child/Counter.vue'
    import Basket from './child/Basket.vue'
    import axios from "axios";
    const test = ref('change-count')

    const callback = data => test.value = data
    function addBuySession(id) {
        alert(test.value);
        if(test.value=='change-count'){
            test.value = 1;
        }
        const formData = new FormData();
        formData.append("Id", id);
        if (getCookie("session") == undefined) {

            axios.post(API_URL + "api/Basket/api/Add?products=" + id + "&count=" + test.value).then(
                (response) => {

                    setCookie("session", response.data, {secure: true, 'max-age': 86300});
                    alert(response.data + 'not cocke');
                }
            )




        }
        else if (getCookie("session").length >0) {

                axios.post(API_URL + "api/Basket/api/Add?products=" + id + "&sessionId=" + getCookie("session") + "&count=" + test.value).then(
                    (response) => {
                        if (response.data != getCookie("session")) {
                            setCookie("session", response.data, { secure: true, 'max-age': 86300 });
                        }

                    }
                )}


        }
function addBuyUser(id,user){
    axios.post(API_URL + "api/Basket/api/Add?products=" + id + "&count=" + test.value + '&username=' + user).then(
        (response) => {


            alert(response.data)
        }
    )
}


</script>

<template>

    <div class="grid-x grid-padding-x">
        <div class="cell medium-4 large-3" id="item1" v-for="(item,id) in item" :key="item.id">

            <img :src="item.preview" alt="Упс изображение не загрузилось" class="cardimage">
            <div class="small-12">
                <h1>{{item.title}}</h1>
                <p>Цена:{{item.coast}} РУБ</p>
                <p>{{content.DescriptionLabel}}</p>
                <p>{{item.description}}</p>
            </div>
            <div class="grid-x">
                <br />
                <div class="small-12" id="countstitle">{{content.CountLabel}}</div>
                <br />
                <br />
                <Counter @change-count="callback" />


                <br />

                <div class="medium-12">
                    <button id="addBuy" v-if="!isAuth" @click="addBuySession(item.id)">{{content.addBuyRU}}</button>
                    <button id="addBuy" v-if="isAuth" @click="addBuyUser(item.id,gettersAuthData.userName)">{{content.addBuyRU}}</button>
                </div>
            </div>
        </div>



    </div>
{{isAuth}}
</template>

<script>

    import emitter from 'tiny-emitter/instance'
    import { ref } from 'vue'
    import {mapGetters} from 'vuex';
    import {getnamepi} from '@/apioptions/index.js'
    import axios from "axios";
    const API_URL = "http://localhost:5000/"
    function getCookie(name) {
        let matches = document.cookie.match(new RegExp(
            "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
        ));
        return matches ? decodeURIComponent(matches[1]) : undefined;

    }
    function getap(){
        return getnamepi()
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
    const routes = [{ path: '/counter', component: Counter }]
    export default
        {
            components: {
                Counter,



            },
            data() {
                return {

                    item: [],
                    content: {
                        addBuyRU:'Добавить в корзину',
                        DescriptionLabel:'Описание',
                        CountLabel:'Количество',
                    },
                    isAuth:false,


                }
            },
            computed: {
                ...mapGetters('auth', {
                    gettersAuthData: 'getAuthData'
                })},


            methods: {
                async refreshData() {
                    axios.get(API_URL + "api/Product").then(
                        (response) => {
                            const data = response.data;
                            this.item = data;


                        }
                    )

                },


                async authget(){
                    if(this.gettersAuthData.userName==""){
                        this.isAuth=false;
                    }
                    else if(this.gettersAuthData.userName!=""){
                        this.isAuth=true;
                    }
                },





            },

            created() {
                this.refreshData();
                this.timer = setInterval(this.refreshData, 10000);
            },
            mounted: function () {
                this.refreshData();
                this.authget();

            },


        }





</script>