<template>
<div class="grid-container">
    <div class="grid-x grid-padding-x">
      <div class="medium-10" v-for="(item,id) in orders" :key="item.id">
        <div v-if="gettersAuthData.role=='Кухонный работник'&& item.status=='Оплачен'">
            <h4>
                Заказ №{{item.id}}

            </h4>
            <p>Наименование:{{item.product}}</p>
            <p>Адрес доставки:{{item.address}}</p>
            <p>ФИО: {{item.snm}}</p>
            <p>Номер телефона: {{item.phone}}</p>
            <p>Количество:<CountPR :id="item.productId" :baskets="item.basketId"></CountPR></p>
            <p>Статус:{{item.status}}</p>
        </div>
          <div v-if="gettersAuthData.role=='Администратор' || gettersAuthData.role=='Менеджер'">
              <h4>
                  Заказ №{{item.id}}

              </h4>
              <p>Наименование:{{item.product}}</p>
              <p>Адрес доставки:{{item.address}}</p>
              <p>ФИО: {{item.snm}}</p>
              <p>Номер телефона: {{item.phone}}</p>
              <p>Количество:<CountPR :id="item.productId" :baskets="item.basketId"></CountPR></p>
              <p>Комментарий к заказу:{{item.comments}}</p>
              <p>Статус:{{item.status}}</p>
              <Cancel :baskets="item.basketId"></Cancel>
              <StatusBTN :baskets="item.basketId" :Status="item.status" :Role="gettersAuthData.role"></StatusBTN>

          </div>


      </div>
    </div>
</div>
</template>

<script>
import axios from "axios";
import {mapGetters} from "vuex";
import CountPR from '@/components/child/CountProductBasket.vue'
import Cancel from '@/components/child/CancelButtonOrder.vue'
import StatusBTN from '@/components/child/ButtonStatusManager.vue'

const API_URL = "https://localhost:7084/";
export default {
    name: "OrdersPanel",


        data(){
        return{
            role:'',
            orders:[]
        }
    },
    components:{
      CountPR,
        Cancel,
        StatusBTN
    },
    methods:{
        refresh(){
            axios.get(API_URL + "api/Order/Get").then(
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
        this.timer = setInterval(this.refresh, 1000);
    },

    mounted() {
        this.refresh();
        setTimeout(this.refresh, 1000);
    }
}
</script>

<style scoped>

</style>