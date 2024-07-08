class Message {
    constructor(username, text, when) {
        this.userName = username;
        this.text = text;
        this.when = when;
    }
}

// userName is declared in razor page.
const username = userName;
const textInput = document.getElementById('messageText');
const groupId = "Private"+document.getElementById('groupId').value;
const memberIdFileName = document.getElementById('memberIdFileName');
const MemberIdPath = document.getElementById('memberIdPath');
const whenInput = document.getElementById('when');
const chat = document.getElementById('chat');
const messagesQueue = [];

document.getElementById('submitButton').addEventListener('click', () => {
    var currentdate = new Date();
    when.innerHTML =
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })
});

function clearInputField() {
    messagesQueue.push(textInput.value);
    textInput.value = "";
}

function sendMessage() {

    if (groupId !== "Private") {
        SendMessageToGroup();
        return;
    }
    /*let text = messagesQueue.shift() || "";
    if (text.trim() === "") return;

    let when = new Date();
    let message = new Message(username, text);
    connection.invoke("SendMessage", message).catch(function (err) {
        return console.error(err.toString());
    });*/
}

function SendMessageToGroup() {
    let text = messagesQueue.shift() || "";
    if (text.trim() === "") return;

    var PrivateGroup = groupId;
    let when = new Date();
    let message = new Message(username, text);
    connection.invoke("SendMessageToGroup", PrivateGroup, message).catch(function (err) {
        return console.error(err.toString());
    });
}

function addMessageToChat(message) {
    let isCurrentUserMessage = message.userName === username;
    

    let container = document.createElement('div');
    container.className = isCurrentUserMessage ? "chat-message-right pb-4" : "chat-message-left pb-4";


    var divImage = document.createElement("div");
    var img = document.createElement("img");

    img.src = "/images/Profile/" + memberIdFileName.value;
    img.className = isCurrentUserMessage ? "rounded-circle mr-1" : "rounded-circle mr-1";
    img.style.width = '40px';
    img.style.height = '40px';
    img.id = "picture";

    


    

    let when = document.createElement('div');
    when.className = isCurrentUserMessage ? "text-muted small text-nowrap mt-2" : "text-muted small text-nowrap mt-2";
    var currentdate = new Date();
    when.innerHTML =
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true })

    divImage.appendChild(img);
    divImage.appendChild(when);

    var nameWithTxt = document.createElement('div');
    nameWithTxt.className = isCurrentUserMessage ? "flex-shrink-1 bg-light rounded py-2 px-3 mr-3" : "flex-shrink-1 bg-light rounded py-2 px-3 mr-3";

    let sender = document.createElement('div');
    sender.className = isCurrentUserMessage ? "font-weight-bold mb-1" : "font-weight-bold mb-1";
    if (container.className === "chat-message-right pb-4") {
        sender.innerHTML = "You";
    } else {
        sender.innerHTML = message.userName;
    }
    
    let text = document.createElement('p');
    text.innerHTML = message.text;

    nameWithTxt.appendChild(sender);
    nameWithTxt.appendChild(text);

    container.appendChild(divImage);
    container.appendChild(nameWithTxt);
    chat.appendChild(container);
} 


document.getElementById("joinGroup").addEventListener('click', function (event) {
    connection.invoke("JoinGroup", groupId).catch(function (err) {
        return console.error(err.toString());
    });

});
