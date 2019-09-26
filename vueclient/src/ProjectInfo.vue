<template>
    <div id="projectinfo" class="container">
        <ul class="list-group">
            <li class="list-group-item">
                
                <label>Название компании-заказчика</label>
                <input type="text" readonly name="" id="projectCustomer" :value="project.customer">
                
            </li>
            <li class="list-group-item">
                
                <label>Название компании-исполнителя</label>
                <input type="text" readonly name="" id="projectPerformer" :value="project.performer">
               
            </li>
            <li class="list-group-item">
                
                <label>Руководитель проекта</label>
                <input type="text" readonly name="" id="projectLead" :value="lead">
                
            </li>
            <li class="list-group-item">
               
                <label>Дата начала проекта</label>
                <input type="text" readonly name="" id="projectStart" :value="project.startDate">
               
            </li>
            <li class="list-group-item">
               
                <label>Дата окончания проекта</label>
                <input type="text" readonly name="" id="projectFinish" :value="project.finishDate">
               
            </li>
            <li class="list-group-item">
              
                    <label >Приоритет проекта</label>
                    <input type="text" readonly name="" id="projectPriority" :value="project.priority">
             
            </li>
        </ul>
  </div>
</template>

<script>
    import axios from 'axios'
    export default {
        name: 'projectinfo',
        data: function(){
            return{
               employees: [],
               lead: String
            }
        },
        props:{
            project: Object,
        },
        created(){
            if(this.project.leadId != null)
                axios.get('https://localhost:44316/api/employee/'+this.project.leadId)
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
                            //console.log(response.data);
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