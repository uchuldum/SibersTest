<template>
    <div id="projectinfo" class="container">
        <ul class="list-group">
            <li class="list-group-item">
                <div class="row">
                    <label class = "col-sm">Название проекта</label>
                    <input  class = "col-sm" type="text" name="" id="projectCustomer" :value="project.name">
                </div>
            </li>
            <li class="list-group-item">
                <div class="row">
                    <label class = "col-sm">Название компании-заказчика</label>
                    <input  class = "col-sm" type="text" name="" id="projectCustomer" :value="project.customer">
                </div>
            </li>
            <li class="list-group-item">
                <div class="row">
                <label class = "col-sm">Название компании-исполнителя</label>
                <input class = "col-sm" type="text" name="" id="projectPerformer" :value="project.performer">
                </div>
            </li>
            <li class="list-group-item">
                <div class="row">
                 <label class="col-sm">Руководитель проекта</label>
                    <findemployee inputType="radio" v-model="project.lead" class="col-sm"></findemployee>
                </div>
            </li>
            <li class="list-group-item">
                <div class="row">
                    <label class = "col-sm">Дата начала проекта</label>
                    <input class = "col-sm" type="text" name="" id="projectStart" :value="project.startDate"> 
                </div>
            </li>
            <li class="list-group-item">
                <div class="row">
                    <label class = "col-sm">Дата окончания проекта</label>
                    <input class = "col-sm" type="text" name="" id="projectFinish" :value="project.finishDate">
                </div>
            </li>
            <li class="list-group-item">
                <div class="row">
                    <label class = "col-sm" >Приоритет проекта</label>
                    <input class = "col-sm" type="text" name="" id="projectPriority" :value="project.priority">
                </div>
            </li>
        </ul>
        <div class="row mt-2">
            <input class="col-md" type="button" value="Редактировать проект" @click="editProject">
        </div>
        <div class="row mt-2">
            <input class="col-sm" type="button" value="Удалить проект">
        </div>
  </div>
</template>

<script>
    import axios from 'axios'
    import findemployee from './FindEmployee.vue'

    export default {
        //: 'projectinfo',
        components:{
            findemployee
        },
        data: function(){
            return{
                oldProject: Object,
                newProject: {
                    customer: String,
                    performer: String,
                    leadId: Number,
                    startDate: Date,
                    finishDate: Date,
                    priority: Number
                },
                employees: [],
                lead: String
            }
        },
        props:{
            project : Object
        },
        methods:{
            editProject(){
                
                this.newProject.customer = this.project.customer;

               /* this.newProject.performer =
                this.newProject.leadId =
                this.newProject.startDate =
                this.newProject.finishDate =
                this.newProject.priority =*/

                console.log(JSON.stringify(this.project));
            }
        },
        created(){
                this.oldProject = this.project;
                if(this.project.leadId != null)
                axios.get('https://localhost:44316/api/employee/getid/'+this.project.leadId)
                    .then(response => {
                        this.lead = response.data.surName + " "+ response.data.name,
                        console.log(response.data)
                    })
                    .catch(error => {
                        console.log(error);
                    })
                else
                    this.lead = "Руководителя нет"
                if(this.project.projectId != null)
                    axios.get('https://localhost:44316/api/projectsemployees/'+this.project.projectId)
                        .then(response => {
                            this.employees = response.data;
                            console.log(this.employees);
                        })
                        .catch(error => {
                            console.log(error);
                        })
        }    
    }
    
</script>
<style>
    .switch-transition{
        transition: all .5s ease-in-out;
    }
    .switch-enter{
      bottom: 100%;
    }
    .switch-leave{
      bottom: -100%;
    }
</style>