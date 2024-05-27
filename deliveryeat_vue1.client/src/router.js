import {createRouter, createWebHistory} from 'vue-router';
import Home from "@/components/Home.vue";
import NotFound from "@/components/NotFound.vue";
import Basket from "@/components/Basket.vue";

import store from "@/store/index.js";


const routes = [
    {
        name: 'Home',
        path: '',
        component: Home,
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
        name: 'Basket',
        path: '/basket',
        component: Basket,
        meta: {
            requiresAuth: false
        }


    },
    {
        name: 'Login',
        path: '/login',
        component: import('@/components/Login.vue'),
        meta: {
            requiresAuth: false
        }
    },
    {
            name: 'Basketuser',
        path: '/basketuser',
        component: import('@/components/BasketUser.vue'),
        meta: {
            requiresAuth: true
        }
    },
    {
        name: 'PayOrderUser',
        path: '/payorder/:id',
        component: import('@/components/PayOrderUser.vue'),
        meta: {
            requiresAuth: false
        }
    },
    {
        name: 'OrderPanel',
        path: '/orderPanel',
        component: import('@/components/OrdersPanel.vue'),
        meta: {
            requiresAuth: true
        }
    },
    {
        name: 'Courier',
        path: '/Courier',
        component: import('@/components/DashboardCourier.vue'),
        meta: {
            requiresAuth: true
        }
    },
    {
        name: 'Register',
        path: '/Register',
        component: import('@/components/Register.vue'),
        meta: {
            requiresAuth: false
        }
    }

];

export const routeConfig = createRouter({
    history: createWebHistory(),
    routes: routes,
});
export default routeConfig;

routeConfig.beforeEach((to,from, next) => {
    console.log(store.getters["auth/getAuthData"].token);
    if (!store.getters["auth/getAuthData"].token) {
        const access_token = localStorage.getItem("access_token");

        if (access_token) {
            const data = {
                access_token: access_token,

            };
            store.commit('auth/saveTokenData', data);
        }
    }
    const auth = store.getters["auth/isTokenActive"];

    if (to.fullPath == "/") {
        return next();
    } else if (auth && !to.meta.requiredAuth && to.fullPath == "/login") {
        return next({path: "/basket"});
    } else if (!auth && to.meta.requiredAuth) {
        return next({path: "/login"});
    }

    return next();
});









