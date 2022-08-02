import { defineStore } from "pinia";
export const useProjectStore = defineStore('project', {
    state: () => ({
        basicSettingDesc:'',
        totalCost: 0,
        project: {
            "id": 0,
            "name": "",
            "reference": "",
            "lastUpdate": "",
            "startDate": "",
            "fieldingPeriod": 0,
            "status": 0,
            "testingUrl": "",
            "liveUrl": "",
            "categories": [],
            "user": {
                "id": 0,
                "name": "",
                "email": ""
            },
            "projectTargetAudiences": []
        },
        saveProjectLoading: false,
        saveProjectError: null,
        categories:[],
        loading: false,
        error: null,

    }),
    getters: {
    },
    actions: {
        async CreateProject(project) {
            var currProject = this.project;
            currProject.tempId = this.project.id;
            currProject.lastUpdate = new Date();
            delete currProject.id;

            this.saveProjectLoading = true;
            var iseUrl = import.meta.env.VITE_ISE_API_URL;
            var saveProjectPath = import.meta.env.VITE_ISE_API_SAVEPROJECT;
            const settings = { 
                method: 'POST',
                headers: {
                    Accept: 'application/json',
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(currProject)
            }
            try {
                var savedProject = await fetch((iseUrl + saveProjectPath), settings)
                .then((response) => response.json());
                project.id = savedProject.id;
                project.lastUpdate = savedProject.lastUpdate;
            } catch (error) {
                this.saveProjectError = error;
            } finally {
                this.saveProjectLoading = false;
            }
        },
        async GetProjectSetting(id) {
            // alert('this is test');
        },
        AddTargetAudienceElement() {  
            var id = this.project.projectTargetAudiences.length;
            var ta = {
                "id": id+1,
                "name": "",
                "audienceNumber":0,
                "estimatedIR": 0,
                "estimatedLOI": 0,
                "costPerInterview": 0,
                "cptg": 0,
                "wantedCompletes": 0,
                "qualifications": [],
                "quota": [],
                "quotas": [],
                "subtotal": 0
            };
            this.project.projectTargetAudiences.push(ta)
        },
        CancelTargetAudience(ta) {
            var removeIndex = this.project.projectTargetAudiences.map(item => item.id).indexOf(ta.id);
            ~removeIndex && this.project.projectTargetAudiences.splice(removeIndex, 1);
        },
        LoadProjectQualification() {
            return [
                {
                    "id": 1,
                    "order": 1,
                    "logicalDecision": "OR",
                    "NumberOfRequiredConditions": 0,
                    "IsActive": true,
                    "question": {
                        "id": 42,
                        "name": "Age",
                        "text": "Enter age range for the project",
                        "categoryName": "Main",
                        "variables": [
                            {
                                "id": 1,
                                "name": "18 - 60"
                            }
                        ]
                    }
                },
                {
                    "id": 2,
                    "order": 2,
                    "logicalDecision": "OR",
                    "NumberOfRequiredConditions": 0,
                    "IsActive": true,
                    "question": {
                        "id": 1,
                        "name": "Country",
                        "text": "Enter the Countries",
                        "categoryName": "Main",
                        "variables": [
                            {
                                "id": 1,
                                "name": "UK"
                            }
                        ]
                    }
                },
                {
                    "id": 3,
                    "order": 3,
                    "logicalDecision": "OR",
                    "NumberOfRequiredConditions": 0,
                    "IsActive": true,
                    "question": {
                        "id": 43,
                        "name": "Gender",
                        "text": "Enter the genders of panelists",
                        "categoryName": "Main",
                        "variables": [
                            {
                                "id": 1,
                                "name": "Male"
                            },
                            {
                                "id": 2,
                                "name": "Female"
                            }
                        ]
                    }
                }
            ];
        },
        /*LoadProjectQuota() {
            return [];
        },
        AddQualificationElement(quals) {
            console.log("Came to add qualifications")
            var qual = {
                "id": "",
                "name": "",
                "condition":"",
                "order": 0,
                "isActive": true
            };
            quals.push(qual)
        },
        AddQuotaElement(quots) {
            var quot = {
                "id": "",
                "name": "",
                "condition": "",
                "limit": 0,
                "limitType": "",
                "isActive": true
            };
            quots.push(quot);
        },*/
        CalculateCharges() {
            if(this.project.projectTargetAudiences !== undefined)
            {
                console.log("came in running CPI cost");
                this.totalCost = 0;
                for(var i =0; i < this.project.projectTargetAudiences.length; i++) {
                if(this.project.projectTargetAudiences[i].wantedCompletes > 0 && this.project.projectTargetAudiences[i].estimatedIR > 0 && this.project.projectTargetAudiences[i].estimatedLOI > 0) {
                    var ir = this.project.projectTargetAudiences[i].estimatedIR; var loi = this.project.projectTargetAudiences[i].estimatedLOI
                    if(ir>= 75 && ir <=100 && loi >0 && loi <=5)
                        this.project.projectTargetAudiences[i].costPerInterview = 2;
                    else if(ir>= 50 && ir <=74 && loi >0 && loi <=5)
                        this.project.projectTargetAudiences[i].costPerInterview = 2.5;
                    else if(ir>= 35 && ir <=49 && loi >0 && loi <=5)
                        this.project.projectTargetAudiences[i].costPerInterview = 3;
                    else if(ir>= 11 && ir <=34 && loi >0 && loi <=5)
                        this.project.projectTargetAudiences[i].costPerInterview = 4;
                    else if(ir>= 1 && ir <=10 && loi >0 && loi <=5)
                        this.project.projectTargetAudiences[i].costPerInterview = 7;
                    else if(ir>= 75 && ir <=100 && loi >5 && loi <=10)
                        this.project.projectTargetAudiences[i].costPerInterview = 2.5;
                    else if(ir>= 50 && ir <=74 && loi >5 && loi <=10)
                        this.project.projectTargetAudiences[i].costPerInterview = 3;
                    else if(ir>= 35 && ir <=49 && loi >5 && loi <=10)
                        this.project.projectTargetAudiences[i].costPerInterview = 4;
                    else if(ir>= 11 && ir <=34 && loi >5 && loi <=10)
                        this.project.projectTargetAudiences[i].costPerInterview = 7;
                    else if(ir>= 1 && ir <=10 && loi >5 && loi <=10)
                        this.project.projectTargetAudiences[i].costPerInterview = 8;
                    else if(ir>= 75 && ir <=100 && loi >10 && loi <=15)
                        this.project.projectTargetAudiences[i].costPerInterview = 3;
                    else if(ir>= 50 && ir <=74 && loi >10 && loi <=15)
                        this.project.projectTargetAudiences[i].costPerInterview = 3.5;
                    else if(ir>= 35 && ir <=49 && loi >10 && loi <=15)
                        this.project.projectTargetAudiences[i].costPerInterview = 4.5;
                    else if(ir>= 11 && ir <=34 && loi >10  && loi <=15)
                        this.project.projectTargetAudiences[i].costPerInterview = 7.5;
                    else if(ir>= 1 && ir <=10 && loi >10 && loi <=15)
                        this.project.projectTargetAudiences[i].costPerInterview = 8.5;
                    else 
                        this.project.projectTargetAudiences[i].costPerInterview = 10;
                    var subT = this.project.projectTargetAudiences[i].costPerInterview * this.project.projectTargetAudiences[i].wantedCompletes;
                    this.project.projectTargetAudiences[i].cptg = subT;
                    this.project.projectTargetAudiences[i].subtotal = subT;
                    this.totalCost = this.totalCost + subT;
                }
            }
            }
        }
    }
})