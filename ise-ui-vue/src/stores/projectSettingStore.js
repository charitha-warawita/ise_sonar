import { defineStore } from "pinia";
import { useProjectStore } from "./projectStore"; 

export const useProjectSettingStore = defineStore('projectSetting', {
    state: () => ({
        categories:[],
        loading: false,
        error: null
    }),
    getters: {
    },
    actions: {
        async FetchCategories() {
            this.categories = []
            this.loading = true
            try {
                this.categories = await fetch('https://eo2intbiweb01.corp.gmi.lcl/iseapi/api/Reference/project/categories')
                .then((response) => response.json())
            } catch (error) {
                this.error = error
            } finally {
                this.loading = false
            }
        }
    }
})