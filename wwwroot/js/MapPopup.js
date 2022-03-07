const template = document.createElement('template');
template.innerHTML = `
<style>
.popup {
    background-color: white;
    width: 424px;
    overflow: hidden;
    margin-bottom: 30px;
}
.popup-content{
padding:12px;
}
.pop-title{
    padding-left: 12px;

}

.wave-trans {
width: 100%
height: 100px;
  left: 0;
  margin: auto;
  position: absolute;
  right: 0;
  text-align: center;
  top: 72px;
z-index: 1;
}



.pop-title h1 {
    font-size: 24px;
    height: 40px;
      position:relative;
   z-index: 2;


}
.popup h2{
    font-size: 20px
    margin-top: 0;
}
.popup h2, h3 {
    font-weight: 300;
margin-bottom: 0;
}
.popup h3 {
   width: 60%;
}
.pop-desc{
    padding-top: 5px;
    position: relative;
    padding-left: 12px;
    background-color: #fff;
    height: 340px;
}

.householdInfo dl{
font-size: 16px;
font-weight: 300;
width: 100%;
float:left;
}
.householdInfo dd{
    width: 150px;
 
}
.householdInfo dt{
font-weight: 400;
    width: 110px;
    padding-left: 6px;
float:left;

}
.pop-desc li {
    width: 90px;
}
.list-time{
    float:left;
}

.list-time > p{
display: inline-block;
margin: 0 10px;

font-size: 17px;
    font-weight: 300;
}
ul{
list-style:none;
}
#groceryName{
margin-top: 60px;
}
}


</style>
<div class="popup" id="popup">
    <div class="pop-title" id="pop-title">
        <h1 id="FullAddress"></h1>
    </div>
    <div class="pop-desc">
        <h2><a id="link">Household</a></h2>
        <div class="row householdInfo">
            <dl>
                <dt >Family Name:</dt>
                <dd id="familyName"></dd>
                <dt >Family Size:</dt>
                <dd id="size"></dd>
                <dt>Income:</dt>
                <dd id="income"></dd>
            </dl>
        </div>
        <div class="row">
            <h2 style="text-align: left;">Travel</h2>
            <h3>Grow Community Center</h3>
            <ul>
                <li class="list-time">
                    <img src="/svg/distance.svg" alt="Distance to Grow community Center, in meters"/>
                    <p id="growDistance"></p>
                </li>
                <li class="list-time">
                    <img src="/svg/drive.svg" alt="Time to Drive to Grow Community Center, in seconds"/>
                    <p id="growDrive"></p>
                </li>
                <li class="list-time">
                    <img src="/svg/bike.svg" alt="Time to bike"/>
                    <p id="growBike"></p>
                </li>
                <li class="list-time">
                    <img src="/svg/walk.svg" alt="time to walk"/>
                    <p id="growWalk"></p>
                </li>
            </ul>
            <h3 name="groceryName" id="groceryName"></h3>
            <ul>
                <li class="list-time">
                    <img src="/svg/distance.svg" alt="Distance to nearest grocery store, in meters"/>
                    <p id="groceryDistance"></p>
                </li>
                <li class="list-time">
                    <img src="/svg/drive.svg" alt="Time to Drive to nearest grocery store, in seconds"/>
                    <p id="groceryDrive"></p>
                </li>
                <li class="list-time">
                    <img src="/svg/bike.svg" alt="time to bike"/>
                    <p id="groceryBike"></p>
                </li>
                <li class="list-time">
                    <img src="/svg/walk.svg" alt="time to walk"/>
                    <p id="groceryWalk"></p>
                </li>
            </ul>
  
</div>
 
</div>
</div>
`;
class pop extends HTMLElement {
    constructor() {
        super();
        this.attachShadow({ mode: 'open' });
        this.shadowRoot.appendChild(template.content.cloneNode(true));
        this.shadowRoot.getElementById('FullAddress').innerText = this.getAttribute('name');
        this.shadowRoot.getElementById('income').innerText = this.getAttribute('data-income');
        this.shadowRoot.getElementById('size').innerText = this.getAttribute('data-size');
        this.shadowRoot.getElementById('familyName').innerText = this.getAttribute('data-familyName');

        this.shadowRoot.getElementById('growDistance').innerText = this.getAttribute('data-growDistance');
        this.shadowRoot.getElementById('growDrive').innerText = this.getAttribute('data-growDrive');
        this.shadowRoot.getElementById('growBike').innerText = this.getAttribute('data-growBike');
        this.shadowRoot.getElementById('growWalk').innerText = this.getAttribute('data-growWalk');
        this.shadowRoot.getElementById('groceryName').innerText = this.getAttribute('data-groceryName');
        this.shadowRoot.getElementById('groceryDistance').innerText = this.getAttribute('data-groceryDistance');
        this.shadowRoot.getElementById('groceryDrive').innerText = this.getAttribute('data-groceryDrive');
        this.shadowRoot.getElementById('groceryBike').innerText = this.getAttribute('data-groceryBike');
        this.shadowRoot.getElementById('groceryWalk').innerText = this.getAttribute('data-groceryWalk');
        this.shadowRoot.getElementById('pop-title').style.backgroundColor = this.getAttribute('data-background');
        this.shadowRoot.getElementById('popup').style.backgroundColor = this.getAttribute('data-background');
        this.shadowRoot.getElementById('link').href = `Households/Details/${this.getAttribute('data-id')}`;

    }
}


/*

    < div class="wave-trans" >
    <img src="/svg/purpleWaves.svg" alt="" />
    </div >*/
