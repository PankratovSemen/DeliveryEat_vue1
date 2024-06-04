<template>
<div class="grid-container">
    <div class="grid-y">
       <div class="medium-12 cell">
           <label>Название
           <input type="text" v-model="title"></label>
       </div>
        <div class="medium-12 cell">
            <label>Ссылка на изображение
                <input type="text" v-model="image"></label>
        </div>
        <div class="medium-12 cell">
            <label>Описание
                <textarea v-model="description"></textarea> </label>
        </div>
        <div class="medium-12 cell">
            <label>Цена
                <input type="number" v-model="coast"></input> </label>
        </div>
        <div class="medium-6">
            <button class="button" @click="submit">Добавить</button>
        </div>
    </div>
</div>
</template>

<script>
import axios from "axios";
const API_URL = 'http://localhost:5000/';
export default {

    name: "AddProduct",

    data(){
        return{
            title:'',
            image:'',
            description:'',
            coast:0
        }
    },
    methods:{
        async submit(){
            await axios.post(API_URL + 'api/Product/AddProduct?Title=' + this.title + '&Preview=' + this.image + '&Description='+ this.description + '&Coast=' + this.coast + '&Visability=Visible');
        }
    }
}
</script>

<style scoped>
textarea{
    height:200px;
}
</style>