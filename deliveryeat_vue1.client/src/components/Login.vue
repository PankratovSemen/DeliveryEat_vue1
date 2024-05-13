<template>
<div class="grid-x grid-">
    <div class="cell medium-4 large-4">

    </div>
    <div class="cell medium-4 large-4 register">
        <h3>Авторизация</h3>
        <div>
        <form>
            <br>
            <p>Логин</p>
            <input v-model="username">
            <br>
            <br>
            <p>Пароль</p>
            <input v-model="password">
            <br>
            <br>
            <p  @click="login" class="button">Войти</p>

        </form>
        </div>
    </div>
    <div class="cell medium-4 large-4">

    </div>
</div>
</template>

<script>
import {mapActions, mapGetters} from "vuex";

export default {

    name: "Login",
    data(){
        return{


            username:'',
            password:''
        }
    },
    computed:{
        ...mapGetters('auth',{
            getterLoginStatus:'getLoginStatus'
        })
    },
    methods:{
        ...mapActions('auth',{
            actionLogin:'login'
        }),
        async login(){
            await this.actionLogin([this.username,this.password]);
            if(this.getterLoginStatus === 'success'){
                this.$router.push("/basket");
            }else{
                alert('failed to login')
            }
        }
    }


}
</script>

<style scoped>
* {
    outline: none;
}
.register{
    margin-top: 10px;
    padding: 3%;

    min-width: 250px;
    padding: 20px;
    border-left: 8px solid #c99e10;
    border-right: 8px solid #cb3f2a;
    background-image: linear-gradient(45deg, #c99e10, #3020ff), linear-gradient(45deg, #c99e10, #cb3f2a);
    background-size: 100% 6px;
    background-position: 0 0, 0 100%;
    background-repeat: no-repeat;
    border-radius: 25px;
    font-weight: bold;
    font-family: Arial;

}

input{
    width: 100%;
    border:none;
    border-bottom: 1px solid #0a53be;
    background-color: transparent;
}

button{
    margin-left:40%;
    font-size:16px
}

h3{
    text-align: center;
}
</style>