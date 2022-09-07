import { defineStore } from "pinia";

export const useProjectsStore = defineStore('projects', {
    state: () => ({
        projects:[],
        currentProjects: [],
        currentPageRowCount: 10,
        currentPageNumber: 1,
        selectRowCount: [10,25,50,100],
        totalItems: 0,
        searchByName: '',
        searchByStartDate: '',
        allStatuses:["Draft", "Created", "Active", "Paused", "Complete", "Closed", "Halt", "All"],

        projectListLoading: false,
        projectListError: null,

        currentStatus: 7,
        iseUrl: import.meta.env.VITE_ISE_API_URL,
        iseGetProjectsPath: import.meta.env.VITE_ISE_API_GETPROJECTS


    }),
    getters: {
        getDraftProjects: (state) => {
            return state.projects.filter(project => project.status === 0);
        }
    },
    actions: {
        async setDefaultProjectList() {
            console.log("came into this function");
            this.searchByName = '';
            this.searchByStartDate = '';
            this.currentPageRowCount = 10;
            this.currentProjects = await this.GetProjectsList();
            this.currentStatus = 7;
        },
        async GetProjectsList() {
            var path = 'pageNumber=' + this.currentPageNumber + '&recordCount=' + this.currentPageRowCount;
            if(this.currentStatus > -1 && this.currentStatus < 7)
                path += '&status=' + this.currentStatus;
            if(this.searchByName !== '')
                path += '&searchString=' + this.searchByName;
            try {
                this.projectListLoading = true;
                var result = await fetch(this.iseUrl + this.iseGetProjectsPath + path)
                .then((response) => response.json());
                if(result !== null && Array.isArray(result.projects)) {
                    this.totalItems = result.totalItems;
                    for(var i =0; i < result.projects.length; i++) {
                        result.projects[i].statusValue = this.allStatuses[result.projects[i].status];
                    }
                }
                return result.projects;
            } catch(error) {
                this.projectListError = error;
            } finally {
                this.projectListLoading = false;
            }
        },
        async getProjectsBySearchNameAndStartDate (status) {
            this.currentPageNumber = 1;
            console.log("came in.. " + status);
            if(status === undefined || status === '' || status === -1) {
                this.currentStatus = 7;
            }
            else {
                this.currentStatus = status;
            }
            this.currentPageRowCount = 10;
            this.currentProjects = await this.GetProjectsList();
            

            /*if(this.currentStatus !== 'All') {
                curr = this.projects.filter(project => project.status === this.currentStatus)
            }
            else {
                curr = this.projects
            }
            if(this.searchByName !== '')
                curr = curr.filter(project => (project.name.toLowerCase()).includes(this.searchByName.toLowerCase()))
            if(this.searchByStartDate !== '')
                curr = curr.filter(project => (project.startDate.includes(this.searchByStartDate)))*/
            
            // this.currentProjects = curr;
        },
        async selectedOption(event) {
            this.currentPageRowCount = event.target.value;
            this.currentProjects = await this.GetProjectsList();
        },
    }
})