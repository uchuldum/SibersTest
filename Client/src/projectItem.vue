<template>
    <div class="row pt-2">
        <h4 class="col-sm-4">{{project.name}}</h4>
        <input
         @click ="showModal()"
         type="button" class="btn btn-primary" value="О проекте">
         <modal v-show="isModalVisible" @close="closeModal">
              <div slot="header">
                Данные об {{project.name}}
                <button
                  type="button"
                  class="btn-close"
                  @click="closeModal"
                  aria-label="Close modal"
                  >
                  x
                </button>
              </div>
              <div slot="body">
                <product-details :project="project"></product-details>
              </div>
              <div slot="footer">
                SibersTest Footer
              </div>
            </modal>
    </div>
</template>

<script>

import productDetails from './ProjectInfo.vue'
import modal from "./modalAddEmployee.vue"

export default {
    components:{
        productDetails,
        modal
    },
    props:{
        project: Object
    },
    data(){
        return{
            addremove: true,
            isModalVisible: false
        }
    
    },
    methods:{
        closeModal(){
            if(this.isModalVisible)
                this.isModalVisible = !this.isModalVisible
        },
        showModal(){
             if(!this.isModalVisible)
                this.isModalVisible = !this.isModalVisible
        }

    }
}
</script>
