<script setup>
    import { defineEmits, reactive } from 'vue'
    const emit = defineEmits(['change-count'])
    const counts = reactive({
        value:1
    })

    function plus() {
        counts.value++;
        emit('change-count', counts.value)
    }

    function minus() {
        if (counts.value < 1) {
            counts.value = 1;
        }
        counts.value--;
        emit('change-count', counts.value);
    }

</script>


<template>
    <div class="small-4 cell">
        <button @click="minus" class="submit success button" id="counts">-</button>

    </div>

    <div class="small-4 cell">

        <input type="text" v-model="counts.value" class="small-2" id="counts" />



    </div>

    <div class="small-4 cell">


        <button @click="plus" class="alert button" id="counts">+</button>
    </div>
</template>



<!--<script>
    const emit = defineEmits(['count']);
    export default {
        data() {
            return {
                count:0
            }
        },

        methods: {
            countplus() {
                this.count++;
                emit('count', this.count);
               
            },
            countminus() {
                this.count--;
                emit('count', this.count);
            }
        }
    }
</script>-->
