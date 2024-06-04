<template>
<form data-abide novalidate>
    <div class="grid-container">
        <div class="grid-x grid-padding-x">
            <div class="medium-10 cell">
                <label>Фамилия
                <input v-model="surname" @blur="v$.surname.$touch" type="text"  >
                    <span class="error" v-if="v$.surname.$error">Заполните поле</span>


                </label>
            </div>
            <div class="medium-10 cell">
                    <label>Имя
                        <input v-model="name" @blur="v$.name.$touch" type="text">
                        <span class="error" v-if="v$.name.$error">Заполните поле</span>
                    </label>
            </div>
            <div class="medium-10 cell">
                    <label>Отчество
                        <input v-model="middlename" @blur="v$.middlename.$touch" type="text">
                        <span class="error" v-if="v$.middlename.$error">Заполните поле</span>
                    </label>
            </div>
            <div class="medium-10 cell">
                    <label>Телефон
                        <input v-model="phone" @blur="v$.phone.$touch" type="tel">
                        <span class="error" v-if="v$.phone.$error">Заполните поле</span>
                    </label>
            </div>
            <div class="medium-10 cell">
                    <label>Адрес
                        <input v-model="address" @blur="v$.address.$touch" type="text">
                        <span class="error" v-if="v$.address.$error">Заполните поле</span>
                    </label>
            </div>

            <div class="medium-10 cell">
                    <label>Комментарий
                        <textarea v-model="comment"> </textarea>
                    </label>
            </div>

               <fieldset class="medium-5 cell">
                   <button class="button" type="button" @click="submit">Отправить</button>
               </fieldset>

            </div>
        </div>

</form>
</template>

<script>
import axios from "axios";
import router from "@/router.js";
import {useVuelidate} from '@vuelidate/core'
import {minLength, required} from '@vuelidate/validators'

const API_URL = "http://localhost:5000/";
export default {
    name: "PayOrderUser",
    setup () {
        return {
            v$: useVuelidate()
        }
    },
    data(){
        return{
            phone:'',
            address:'',
            comment:'',
            surname:'',
            name:'',
            middlename:'',
        }
    },
    methods:{
        submit(){
            alert(this.$route.params.id);
            axios.post(API_URL + "api/Order/Create?Surname="+this.surname + "&Name="+this.name + "&MiddleName=" + this.middlename
             + "&Phone="+this.phone + "&Address="+this.address + "&Comments=" + this.comment + "&basketid=" + this.$route.params.id)
        }
    },
    validations () {
        return {
            name: {
                required,

            },
            surname: {
                required,

            },
            middlename: {
                required,

            },

            phone: {
                required,
                minLength:minLength(7)
            },
            address: {
                required,
                minLength:minLength(5)
            },


        }
    },

}
</script>

<style scoped>
.error{
    color:red;
}

</style>