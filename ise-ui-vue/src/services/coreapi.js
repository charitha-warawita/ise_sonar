import auth from './auth'

const CORE_API_BASE = import.meta.env.VITE_ISE_API_URL
const CORE_API_SCOPES = [ import.meta.env.VITE_CORE_API_SCOPE ]


let apiAccessToken

export default {
  async getProjects(path) {
    let iseGetProjectsPath = import.meta.env.VITE_ISE_API_GETPROJECTS;
    let resp = await getCoreApi(iseGetProjectsPath + path);
    if (resp) {
      let data = await resp.json()
      return data
    }
  },
  async getProjectCategories() {
    let path = import.meta.env.VITE_ISE_API_CATEGORIES;
    let resp = await getCoreApi(path);
    if (resp) {
      let data = await resp.json()
      return data
    }
  },
  async performAPIValidation(project) {
    var path = import.meta.env.VITE_ISE_API_CINTREQUEST;
    let resp = await postCoreApi(path, project);
    if (resp) {
      let data = await resp.json()
      return data
    }
  },
  async createProject(project) {
    var path = import.meta.env.VITE_ISE_API_SAVEPROJECT;
    let resp = await postCoreApi(path, project);
    if (resp) {
      let data = await resp.json()
      return data
    }
  },
  async getProfileCategories() {
    var path = import.meta.env.VITE_ISE_API_PROFILECATEGORIES;
    let resp = await getCoreApi(path);
    if (resp) {
      let data = await resp.json()
      return data
    }
  },
  async getQAByCategory(category) {
    var path = import.meta.env.VITE_ISE_API_QABYCATEGORY;
    let resp = await getCoreApi(path + encodeURIComponent(category));
    if (resp) {
      let data = await resp.json()
      return data
    }
  },
  async getApiAccessToken() {
    if(apiAccessToken === undefined)
        apiAccessToken = await auth.acquireToken(CORE_API_SCOPES);
    return apiAccessToken
  }
}

//
// Common fetch wrapper (private)
//
async function getCoreApi(apiPath) {
  // Acquire an access token to call APIs (like Graph)
  // Safe to call repeatedly as MSAL caches tokens locally
  apiAccessToken = await auth.acquireToken(CORE_API_SCOPES)

  let resp = await fetch(`${CORE_API_BASE}${apiPath}`, {
    headers: { authorization: `bearer ${apiAccessToken}` }
  })

  if (!resp.ok) {
    var repBody = await resp.json();
    var text = resp.statusText;
    if(repBody !== null)
        text += '- ' + repBody;
    throw new Error(`Call to ${CORE_API_BASE}${apiPath} failed: ${text}`)
  }

  return resp
}

async function postCoreApi(apiPath, project) {
    apiAccessToken = await auth.acquireToken(CORE_API_SCOPES);
    const settings = { 
        method: 'POST',
        headers: {
            authorization: `bearer ${apiAccessToken}`,
            Accept: 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(project)
    };

    let resp = await fetch(`${CORE_API_BASE}${apiPath}`, settings);
    if (!resp.ok) {
        var repBody = await resp.json();
        var text = resp.statusText;
        if(repBody !== null)
            text += '- ' + repBody;
        throw new Error(`Call to ${CORE_API_BASE}${apiPath} failed: ${text}`)
    }
    return resp;
}