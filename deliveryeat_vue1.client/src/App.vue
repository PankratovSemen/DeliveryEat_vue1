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
                    <button id="addBuy"@click="addBuy">Добавить в корзину</button>
                </div>
            </div>
        </div>



    </div>

</template>

<script>
    
    import emitter from 'tiny-emitter/instance'
    import {ref} from 'vue'
    const API_URL = "https://localhost:7084/"
   
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
                addBuy() {
                    alert(test.value);
                }

              



            },


            mounted: function () {
                this.refreshData();
                

            },

            
        }





</script>


