<template>
    <div class="row pt-2">
        <h4 class="col-sm-6">{{project.name}}</h4>
        <input
         @click ="add()"
         type="button" class="btn btn-primary" value="View Details">
         <div :is="componentName" :project = "project" class="pt-2"></div>
    </div>
</template>

<script>

import productDetails from './ProjectInfo.vue'
export default {
    components:{
        productDetails
    },
    props:{
        project: Object
    },
    data(){
        return{
            componentName: null,
            addremove: true,
        }
    
    },
    methods:{
        add(){
            if(this.addremove)
            {
                this.componentName = "productDetails";
                this.addremove = false;
            }
            else
            {
                this.addremove = true; 
                this.componentName = null;
            }
        }

    }
}
</script>
