
const url = "http://localhost:5090/api/activity";

let myActivities;

async function handleOnLoad(){
    
    let response = await fetch(url);
    let data = await response.json();
    myActivities = data;
    console.log(data);
        
    let html = 
        `
        <div class="p-5 mb-4 bg-body-tertiary rounded-3">
        <div class="container-fluid py-5">
        <h1 class="display-5 fw-bold">Tide Fit</h1>
        <p class="col-md-8 fs-4">Tuscaloosa's first choice in fitness tracking!</p>
        
        </div>
        </div>

            
        <div id="tableBody"></div>

    
        <form onsubmit="return false" class="mt-4">
        <div class="mb-3">
        <label for="activityType" class="form-label">Activity Type:</label>
        <input type="text" id="activityType" name="activityType" class="form-control">
        </div>

        <div class="mb-3">
        <label for="distance" class="form-label">Distance:</label>
        <input type="text" id="distance" name="distance" class="form-control">
        </div>

        <div class="mb-3">
        <label for="date" class="form-label">Date:</label>
        <input type="text" id="date" name="date" class="form-control">
        </div>
        
        <button onclick="handleActivityAdd()" class="btn btn-primary">Submit</button>
        </form>`;
        document.getElementById('app').innerHTML = html;
        populateTable();

}

function populateTable()
{
    console.log(myActivities);
   let html =
   `<table class="table table-striped">
        
        <tr>
            <th scope="col">Activity Type</th>
            <th scope="col">Distance in Miles</th>
            <th scope="col">Date Completed (M/D)</th>
            <th scope="col">Pin</th>
            <th scope="col">Delete</th>
        </tr>
       
   
      
    `

    
    myActivities.sort((a, b) => new Date(b.date) - new Date(a.date));
    
    myActivities.forEach(function(activity){
        
        if(activity.deleted != 1){
            let pinVar = ""
            if(activity.pinned == true){
                pinVar = '❤️'
            }else{
                pinVar = '♡'
            }
            html += 
            `<tr>
            <td>${activity.activityType}</td>
            <td>${activity.distance}</td>
            <td>${activity.date}</td>
            <td><button type="button" class="btn active" data-bs-toggle="button" onclick="handleActivityPin(${activity.id}, ${activity.pinned})">${pinVar}</button></td>
            <td><button class = "btn btn-danger" onclick="handleActivityDelete('${activity.id}')">Delete</button></td>
            
            
            </tr>`;
        }
            
    })

    html+=`</table>`
    document.getElementById('tableBody').innerHTML = html;
}


async function handleActivityAdd()
{

    let activity = {activityType: document.getElementById('activityType').value, distance: document.getElementById('distance').value, date: document.getElementById('date').value, pinned: false, deleted: false};
   console.log(activity)
    await fetch(url, {
        method:"POST",
        body: JSON.stringify(activity),
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    })
    
    document.getElementById('activityType').value = '';
    document.getElementById('distance').value = '';
    document.getElementById('date').value = '';

    await handleOnLoad();
    populateTable();
}

async function handleActivityDelete(index)
{
    
    
        await fetch(url + "/"+index, {
            method:"DELETE",
            headers: {
                "Content-type": "application/json; charset=UTF-8"
            }
        })

    await handleOnLoad();
    populateTable()
    
}

async function handleActivityPin(index, pinned)
{

    await fetch(url + "/"+index, {
        method:"PUT",
        body: JSON.stringify(pinned),
        headers: {
            "Content-type": "application/json; charset=UTF-8"
        }
    })
    await handleOnLoad();
    populateTable();
}

