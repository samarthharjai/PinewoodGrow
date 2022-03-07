const storeTemplate = document.createElement('template');
storeTemplate.innerHTML = `
<style>
.popup {
    background-color: white;
    width: 424px;
    height: 200px;
    margin-bottom: 30px;
    overflow: hidden;
}
.popup-content{
padding:12px;
}
.pop-title{
    padding-left: 12px;

}



.pop-title h1 {
font-size: 20px;
z-index: 2;
}
.popup h2{
    font-size: 18px
    margin-top: 30px;
    font-weight: 300;
}

.pop-desc{
    padding-top: 5px;
    position: relative;
    padding-left: 12px;
    background-color: #fff;
    height: 340px;
}
.pop-desc p{
    font-size: 16px;
}



</style>
<div class="popup" id="popup">
    <div class="pop-title" >
        <h1 id="Name"></h1>
        <h2 id="fullAddress"></h2>
    </div>
    <div class="pop-desc">
        <div class="storeInfo">
            <p id="households"></p>
            <p id="members"></p>
        </div>
</div>
</div>
`;
class storePop extends HTMLElement {
    constructor() {
        super();
        this.attachShadow({ mode: 'open' });
        this.shadowRoot.appendChild(storeTemplate.content.cloneNode(true));
        this.shadowRoot.getElementById('Name').innerText = this.getAttribute('name');
        this.shadowRoot.getElementById('fullAddress').innerText = this.getAttribute('data-fullAddress');
        this.shadowRoot.getElementById('households').innerText = `# Reliant households: ${this.getAttribute('data-households')}`;
        this.shadowRoot.getElementById('members').innerText = `# Reliant members: ${this.getAttribute('data-members')}`;
      

    }
}


/*

    < div class="wave-trans" >
    <img src="/svg/purpleWaves.svg" alt="" />
    </div >*/
