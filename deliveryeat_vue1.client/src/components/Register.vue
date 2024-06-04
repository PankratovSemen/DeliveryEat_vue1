<template>


            <div class="grid-container">
                <div class="grid-x">
                <div class="medium-6 cell">
                    <label>Фамилия
                        <input type="text" v-model="surname">
                    </label>
                </div>

                <div class="medium-6 cell">
                    <label>Имя
                        <input type="text" v-model="name">
                    </label>
                </div>
                <div class="medium-6 cell">
                    <label>Отчество
                        <input type="text" v-model="middlename">
                    </label>
                </div>
                <div class="medium-6 cell">
                    <label>Логин
                        <input type="text" v-model="login">
                    </label>
                </div>
                <div class="medium-6 cell">
                    <label>Пароль
                        <input type="password" v-model="password">
                    </label>
                </div>
                <div class="medium-6 cell">
                    <label>Email
                        <input type="text" v-model="Email">
                    </label>
                </div>

                <button class="button" @click="submit">Отправить</button>
                </div>
            </div>


</template>

<script>
import axios from "axios";
import router from "@/router.js";

const API_URL = "http://localhost:5000/";
export default {
    name: "Register",
    data(){
        return{
            surname:'',
            name:'',
            middlename:'',
            login:'',
            password:'',
            role:'Пользователь',
            Email:'',
        }
    },
    methods:{
       async submit(){
            await axios.post(API_URL + 'Register?surname=' + this.surname + '&name=' + this.name + '&middlename=' + this.middlename + '&login=' + this.login + '&password=' + this.password + '&role=' + 'Пользователь' + '&Email=' + this.Email);
            router.push({path:'/Login'})
        }
    }
}
</script>

<style scoped>

</style>