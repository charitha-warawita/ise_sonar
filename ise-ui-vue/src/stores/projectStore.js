import { defineStore } from "pinia";

export const useProjectStore = defineStore('project', {
    state: () => ({
        basicSettingDesc:'',
        Qualificationlist : [
            {
                "id": 4,
                "order": 4,
                "logicalDecision": "OR",
                "NumberOfRequiredConditions": 0,
                "IsActive": true,
                "question": {
                    "id": 1970,
                    "name": "Online banking",
                    "text": "Are you a user of an internet bank?",
                    "categoryName": "Online/Electronics",
                    "variables": [
                        {
                            "id": 16508,
                            "name": "Yes"
                        },
                        {
                            "id": 16509,
                            "name": "No"
                        }
                    ]
                }
            },
            {
                "id": 5,
                "order": 5,
                "logicalDecision": "OR",
                "NumberOfRequiredConditions": 0,
                "IsActive": true,
                "question": {
                    "id": 1963,
                    "name": "Field of expertise",
                    "text": "Which is your field of expertise?",
                    "categoryName": "Occupation",
                    "variables": [
                        {
                            "id": 16446,
                            "name": "Administration"
                        },
                        {
                            "id": 16447,
                            "name": "Personnel/HR"
                        },
                        {
                            "id": 16449,
                            "name": "IT/Development"
                        },
                        {
                            "id": 16451,
                            "name": "Production"
                        },
                        {
                            "id": 16452,
                            "name": "Management"
                        },
                        {
                            "id": 16453,
                            "name": "Other"
                        },
                        {
                            "id": 22092,
                            "name": "Not Applicable"
                        }
                    ]
                }
            },           
        ],
        
       
        
        totalCost: 0,
        project: {
            "id": "",
            "name": "",
            "reference": "",
            "userId": "",
            "lastUpdate": "",
            "startDate": "",
            "fieldingPeriod": 0,
            "status": "Draft",
            "testingUrl": "",
            "liveUrl": "",
            "categories": [],
            "user": {
                "id": "",
                "name": "",
                "email": ""
            },
            "projectTargetAudiences": [],
            "cintResponseId": 0,
            "cintSelfLink": "",
            "cintCurrentCostLink": "",
            "cintTestingLink": ""
        },
        categories:[],
        loading: false,
        error: null
    }),
    getters: {
    },
    actions: {
        CreateProject(project) {
            console.log('project: ' + JSON.stringify(project));
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
                "qualifications": this.LoadProjectQualification(),
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
        LoadProjectQuota() {
            return [];
                
                /*{
                "id": 1,
                "name": "Quota",
                "fieldTarget": "100",
                "status": true,
                "completes": "",
                "prescreence":"" ,
                    "order": 1,
                    "logicalDecision": "OR",
                    "NumberOfRequiredConditions": 0,
                    "IsActive": true,
                    "condition": {
                        "id": 42,
                        "name": "Age",
                        "text": "Enter age range for the project",
                        "categoryName": "Household",
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
                    "condition": {
                        "id": 1,
                        "name": "Country",
                        "text": "Enter the Countries",
                        "categoryName": "Household",
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
                    "condition": {
                        "id": 43,
                        "name": "Gender",
                        "text": "Enter the genders of panelists",
                        "categoryName": "Household",
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
                }];*/
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
        },
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