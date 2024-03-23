/**
  * теперь этот файл/поток будет кодироваться в UTF-8
  */
import { createApp } from 'vue'
import { createRouter, createWebHistory } from 'vue-router'
import App from './App.vue'
import Indexs from './components/Home.vue';
import Basket from './components/Basket.vue';
import NotFound from './components/NotFound.vue';
import Empty from './components/emptys.vue';

const router = createRouter({
    history: createWebHistory(),
    routes:[
        
        // if you omit the last `*`, the `/` character in params will be encoded when resolving or pushing
        {
            name: 'Empty',
            path: '',
            component: Empty,
            meta: {
                requiresAuth: false
            }


        },
        
        {
            path: "/:catchAll(.*)",
            name: "NotFound",
            component: NotFound,
            meta: {
                requiresAuth: false
            },

        },
        
        {
            name: 'Корзина',
            path: '/basket',
            component: Basket,
            meta: {
                requiresAuth: false
            }


        },
        

    ]
})


const app = createApp(App);
app.use(router);
app.mount("#app");

