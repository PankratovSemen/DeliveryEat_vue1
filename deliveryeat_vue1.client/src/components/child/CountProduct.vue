
    



<template>
    {{count}}
</template>

<script>
 import axios from "axios";

 const API_URL = "http://localhost:5000/";
  function getCookie(name) {
    let matches = document.cookie.match(new RegExp(
        "(?:^|; )" + name.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g, '\\$1') + "=([^;]*)"
    ));
    return matches ? decodeURIComponent(matches[1]) : undefined;

}
function setCookie(name, value, attributes = {}) {

    attributes = {
        path: '/',
        // add other defaults here if necessary
        ...attributes
    };

    if (attributes.expires instanceof Date) {
        attributes.expires = attributes.expires.toUTCString();
    }

    let updatedCookie = encodeURIComponent(name) + "=" + encodeURIComponent(value);

    for (let attributeKey in attributes) {
        updatedCookie += "; " + attributeKey;
        let attributeValue = attributes[attributeKey];
        if (attributeValue !== true) {
            updatedCookie += "=" + attributeValue;
        }
    }

    document.cookie = updatedCookie;
    }
    export default{

        props:['id'],
        data(){

            return{
                count:0,

            }
        },
        methods:{
            getCount(id){


               axios.get(API_URL + "api/Basket/api/GetCountProduct?session=" + getCookie("session") + "&product=" + id).then(
                (response) => {
                    const data = response.data;
                   this.count = data;
                    
                }
               
               );
                
            
            }

           
        },
      mounted: function () {
          this.getCount(this.id);


      },

    }
</script>
