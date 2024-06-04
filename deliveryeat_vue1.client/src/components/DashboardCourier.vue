<template>
    <div class="spec">
        {{username = gettersAuthData.userName}}
    </div>

    <div class="grid-container">
        <div class="grid-x grid-padding-x">
            <div class="medium-10" v-for="(item,id) in orders" :key="item.id">
                <div v-if="gettersAuthData.role=='Курьер' && item.status=='Передано курьеру'">
                    <h4>
                        Заказ №{{item.id}}

                    </h4>
                    <p>Наименование:{{item.product}}</p>
                    <p>Адрес доставки:{{item.address}}</p>
                    <p>ФИО: {{item.snm}}</p>
                    <p>Номер телефона: {{item.phone}}</p>
                    <p>Количество:<CountPR :id="item.productId" :baskets="item.basketId"></CountPR></p>
                    <p>Статус:{{item.status}}</p>
                    <StatusBTN :baskets="item.basketid" :Status="item.status" :Role="gettersAuthData.role"></StatusBTN>

                </div>
            </div>
        </div>
    </div>
</template>

<script>
import axios from "axios";
import {mapGetters} from "vuex";
import CountPR from "@/components/child/CountProductBasket.vue";
import StatusBTN from "@/components/child/ButtonStatusManager.vue";
const API_URL = "http://localhost:5000/";

export default {
    name: "DashboardCourier",
    components: {CountPR,StatusBTN},
    data() {
        return {
            role: '',
            orders: [],
            username:''
        }
    },
    methods:{
        refreshdata(){
            axios.get(API_URL + 'api/Order/GetOrderCourier?username=' + this.username).then(
                (response) => {
                    const data = response.data;
                    this.orders = data;

                }
            )
        }
    },

    computed: {
        ...mapGetters('auth', {
            gettersAuthData: 'getAuthData'
        })},
    created() {
        this.timer = setInterval(this.refreshdata, 1000);
    },

    mounted() {
        this.refreshdata();
    }
}
</script>

<style scoped>
.spec {
    visibility: hidden;
    margin-top: -10px;
}
</style>