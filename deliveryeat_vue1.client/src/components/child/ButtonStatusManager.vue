<template>
<div class="grid-x grid-padding-x">
    <div class="medium-10" v-if="Status=='Обработан'">
        <button class="button" @click="Cooking(baskets)">Приготовлено</button>

    </div>
    <div class="medium-10" v-if="Status=='Заказ готов'">
        <label>Номер курьера <input type="text" v-model="courierid" @blur="v$.courierid.$touch"> </label>
        <span class="error" v-if="v$.courierid.$error">Поле не заполнено или не является числом</span>
        <button class="button" @click="Courier">Передать курьеру</button>
    </div>
    <div class="medium-10" v-if="Status=='Передано курьеру' ">
       <button type="button" class="button" @click="Done">Заказ выдан</button>
    </div>

</div>
</template>

<script>
import axios from "axios";
import {useVuelidate} from "@vuelidate/core";
import {integer, required} from "@vuelidate/validators";
const API_URL = 'https://localhost:7084/';
export default {
    setup () {
        return {
            v$: useVuelidate()
        }
    },
    props: {

        baskets: Number,
        Status:String,
        Role:String
    },
    name: "ButtonStatusManager",
    data(){
        return{
            status:'1',
            courierid:0
        }
    },
    methods:{
        Cooking(){
            axios.post(API_URL + 'api/Order/Cooking?basketId='+ this.baskets)
        },
        Courier(){
            axios.post(API_URL + 'api/Order/Courier?basketId='+ this.baskets + '&userid='+ this.courierid)
        },
        Done(){
            axios.post(API_URL + 'api/Order/Done?basketId='+ this.baskets)
        }
    },
    validations () {
        return {
            courierid: {
                required,
                integer

            },



        }
    }
}
</script>

<style scoped>


.error{
    color:red;
}

</style>