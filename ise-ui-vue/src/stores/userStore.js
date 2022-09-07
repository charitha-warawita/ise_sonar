import auth from '@/services/auth'
import graph from '@/services/graph'
import coreapi from '@/services/coreapi'

import { defineStore } from "pinia";
export const useUserStore = defineStore('user', {
    state: () => ({
        user: {},
        // Access token fetched via MSAL for calling Graph API
        accessToken: '',
        apiAccessToken: '',

        // Details fetched from Graph API, user object and photo
        graphDetails: null,
        graphPhoto: null,
        graphSmallPhoto: null,

        // Visibility toggles for the three details modal popups
        showUserDetails: false,
        showGraphDetails: false,
        showTokenDetails: false,

        // Any errors
        error: ''
    }),
    actions: {
        async created() {
            // Basic setup of MSAL helper with client id, or give up
            if (import.meta.env.VITE_APP_CLIENT_ID) {
              auth.configure(import.meta.env.VITE_APP_CLIENT_ID, false)
        
              // Restore any cached or saved local user
              this.user = auth.user()
              console.log(`configured ${auth.isConfigured()}`)
            } else {
              this.error = 'VITE_APP_CLIENT_ID is not set, the app will not function! 😥'
            }
        },
        /*isAuthenticated() {
            if(this.error !== '')
                return false;
            try {
                this.user = auth.user();
                return (this.user && !this.error);
            }
            catch (err) {
                this.error = err;
                return false;
            }
        },*/

        updateUser() {
            this.user = auth.user()
        },
    
        // Remove locally held user details, is same as logout
        shallowLogout() {
            this.user = null
            this.graphDetails = null
            this.userDetails = null
            this.graphPhoto = null
            this.graphSmallPhoto = null
            auth.clearLocal()
        },
    
        // Full logout local & server side
        fullLogout() {
            this.user = null
            this.graphDetails = null
            this.userDetails = null
            this.graphPhoto = null
            this.graphSmallPhoto = null
            auth.logout()
        },
    
        // Get an access token and call graphGetSelf & graphGetPhoto
        async fetchGraphDetails() {
            if (!this.user || this.user.username == 'demo@example.net') {
                return
            }
        
            try {
                this.graphDetails = await graph.getSelf()
                this.graphPhoto = await graph.getPhoto()
                this.graphSmallPhoto = await graph.getSmallPhoto()
                this.accessToken = graph.getAccessToken()
                this.apiAccessToken = await coreapi.getApiAccessToken()
            } catch (err) {
                this.error = err
            }
        }

    }
})