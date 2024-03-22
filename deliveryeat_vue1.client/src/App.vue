<script setup>
    import { ref } from 'vue'
    import Counter from './components/Counter.vue'
    const test = ref('change-count')

    const callback = data => test.value = data

    
    
</script>

<template>
    <meta charset="UTF-8">
    <header class="grid-x grid-padding-x">
        <div class="large-6 cell">
            <div class="title-bar-left">
                <img src="../img/Menu.png" class="menu">
            </div>
            {{cou}}

        </div>

        <div class="large-6 cell">
            <div class="text-right">
                <img src="../img/cor.png" alt="" class="icon">
                <a href="#" class="secondary button" id="Sigin">Войти</a>

            </div>

        </div>
    </header>


    <div class="grid-x grid-padding-x">
        <div class="cell medium-4 large-3" id="item1" v-for="(item,id) in item" :key="item.id">

            <img src="../img/1.jpg" alt="Упс изображение не загрузилось" class="cardimage">
            <div class="small-12">
                <h1>{{item.title}}</h1>
            </div>
            <div class="grid-x">
                <br />

                <Counter @change-count="callback"/>


                <br />
                <div class="medium-12">
                    <button id="addBuy"@click="addBuy(item.id)">Добавить в корзину</button>
                </div>
            </div>
        </div>



    </div>

</template>

<script>
    
    import emitter from 'tiny-emitter/instance'
    import {ref} from 'vue'
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
            components: {
                Counter
            },
            data() {
                return {

                    item: [],
                    


                }
            },
            

            methods: {
                async refreshData() {
                    axios.get(API_URL + "api/Product").then(
                        (response) => {
                            const data = response.data;
                            this.item = data;


                        }
                    )

                },
                
                addBuy(id) {
                    const formData = new FormData();
                    formData.append("Id", id);
                    if (getCookie("session") == undefined) {
                        axios.post(API_URL + "api/Basket/api/Add?products=" + id).then(
                            (response) => {
                                
                                setCookie("session", response.data, { secure: true, 'max-age': 86300 });
                            }
                        )

                    }
                    else {
                        axios.post(API_URL + "api/Basket/api/Add?products=" + id + "?sessionId=" + getCookie("session")).then(
                            (response) => {
                                if (response.data != getCookie("session")) {
                                    setCookie("session", response.data, { secure: true, 'max-age': 86300 });
                                }
                               
                            }
                        )
                    }
                }

              



            },


            mounted: function () {
                this.refreshData();
                

            },

            
        }





</script>


