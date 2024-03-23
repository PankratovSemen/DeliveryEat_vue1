import { createRouter } from 'vue-router';

import Counter from './components/Counter.vue';


export default createRouter({
    mode: 'history',
    base: process.env.BASE_URL,
    routes: [
        {
            path: '/',
            name: 'home',
            component: Index,
            
        },
    ]
         
    
})