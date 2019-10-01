<template>
    <div>
        <project v-for="project in projects" :key="project.projectId"
        :project="project"></project>
    </div>
</template>

<script>
import axios from 'axios'
import project from './projectItem'
export default {
    components:{
        project
    },
     data () {
        return {
            employees: [],
            projects: []
        }
    },
    created()
    {
      axios.get('https://localhost:44316/api/employee/getid')
      .then(response => {
          this.employees = response.data,
          console.log(this.employees)
      })
      .catch(error => {
          console.log(error)
      }),
      axios.get('https://localhost:44316/api/project/projects')
      .then(response => {
          this.projects = response.data,
          console.log(this.projects)
      })
      .catch(error => {
          console.log(error)
      })
    },
}
</script>