import {jwtDecrypt, tokenAlive} from "@/store/modules/jwtHelper.js";
import axios from "axios";

const state = () => ({
    authData: {
        token: "",
        tokenExp:"",
        role: "",
        userName: "",
    },
    loginStatus:"failed",
});


const getters = {
    getLoginStatus(state){
        return state.loginStatus;
    },
    getAuthData(state){
        return state.authData;
    },
    isTokenActive(state) {

        if (!state.authData.tokenExp) {
            return false;
        }
        return tokenAlive(state.authData.tokenExp);
    },
};

const actions = {
    async login({commit},username) {
        const response = await axios
            .post("http://localhost:5000/Login?login=" + username[0] + "&password="+username[1])
            .catch((err) => {
                console.log(err);
            });
        if (response && response.data) {
            commit("saveTokenData", response.data);
            commit("setLoginStatu", "success");
        } else {
            commit("setLoginStatu", "failed");
        }
    },
};

const mutations = {
    saveTokenData(state, data) {

        localStorage.setItem("access_token", data.access_token);


        const jwtDecodedValue = jwtDecrypt(data.access_token);
        const newTokenData = {
            token: data.access_token,

            tokenExp: jwtDecodedValue.exp,
            role: jwtDecodedValue.role,
            userName: jwtDecodedValue.username,
        };

        state.authData = newTokenData;
    },
    setLoginStatu(state, value){
        state.loginStatus = value;
    }
};

export default{
    namespaced:true,
    state,
    getters,
    actions,
    mutations
}