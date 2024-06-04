




<template>
  {{count}}
</template>

<script>
import axios from "axios";

const API_URL = "http://localhost:5000/";




export default{

    props: {
        id: Number,
        baskets: String
    },
    data(){

        return{
            count:0,

        }
    },
    methods:{
        getCount(id,baskets){


            axios.get(API_URL + "api/Basket/api/GetCountProduct?basketid=" + baskets  +"&product="+id).then(
                (response) => {
                    const data = response.data;
                    this.count = data;

                }

            );


        }


    },
    mounted: function () {
        this.getCount(this.id,this.baskets);


    },

}
</script>
