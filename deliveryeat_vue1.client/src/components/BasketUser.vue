
<template>

    <div class="grid-x grid-padding-x">
        <div class="cell medium-5 large-5" id="item1" v-for="(item,id) in item" :key="item.id">

            <img :src="item.preview" alt="Упс изображение не загрузилось" class="cardimage">
            <div class="small-12">
                <h1>{{item.title}}</h1>
                <p>{{content.DescriptionLabel}}</p>
                <p>{{item.description}}</p>
            </div>
            <div class="grid-x">
                <br />
                <div class="small-12" id="countstitle">{{content.CountLabel}} <CountPR :id="item.id" :user="gettersAuthData.userName"/></div>
                <br />
                <br />



                <br />

            </div>
        </div>

    </div>
    <div class="grid-x grid-padding-x">
        <div class="cell large-12">
            <button class="button" @click="gotopay">Оплатить</button>
        </div>
    </div>

</template>

<script>
import axios from "axios";
import { mapGetters } from 'vuex'

const API_URL = "http://localhost:5000/"
import CountPR from '@/components/child/CountProductUser.vue'
import router from "@/router.js";


export default
{
    computed: {
        ...mapGetters('auth', {
            gettersAuthData: 'getAuthData'
        }),

    },
    components:{
      CountPR
    },
        data() {
            return {

                item: [],
                content: {
                    addBuyRU:'Добавить в корзину',
                    DescriptionLabel:'Описание',
                    CountLabel:'Количество',
                },
                countProduct:0,
                username:'sdd',
                basketid:0

            }
        },


        methods: {
            async refreshData() {


                   axios.get(API_URL + "api/Basket/api/GetBasket?username=" + this.gettersAuthData.userName).then(
                       (response) => {
                           const data = response.data;
                           this.item = data;


                       }
                   )


            },
            async getCount(id) {
                axios.get(API_URL + "api/Basket/api/GetCountProduct?username=" + this.gettersAuthData.userName  +"&product="+id).then(
                    (response) => {
                        const data = response.data;
                        this.countProduct = data;
                        return data;

                    }
                )
            },
            async getbasketid(){
                axios.get(API_URL + "api/Basket/GetBasketId?username=" + this.gettersAuthData.userName).then(
                    (response) => {
                        const data = response.data;
                        this.basketid = data;


                    }
                )
            },
            gotopay(){
                router.push('/PayOrder/'+ this.basketid);
            }






        },


        mounted: function () {

            this.refreshData();
            this.getbasketid();

        },


    }







</script>