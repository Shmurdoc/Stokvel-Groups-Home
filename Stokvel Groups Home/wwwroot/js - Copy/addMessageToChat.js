function addMessageToChat(message) {
    let isCurrentUserMessage = message.userName === username;

    let container = document.createElement('div');
    container.className = isCurrentUserMessage ? "chat-message-right pb-4" : "chat-message-left pb-4";


    var dateWithImage = document.createElement("div");

    let when = document.createElement('div');
    when.className = isCurrentUserMessage ? "text-muted small text-nowrap mt-2" : "text-muted small text-nowrap mt-2";
    var currentdate = new Date();
    when.innerHTML =
        (currentdate.getMonth() + 1) + "/"
        + currentdate.getDate() + "/"
        + currentdate.getFullYear() + " "
        + currentdate.toLocaleString('en-US', { hour: 'numeric', minute: 'numeric', hour12: true });

    dateWithImage.appendChild(when);

    var nameWithTxt = document.createElement('div');
    nameWithTxt.className = isCurrentUserMessage ? "flex-shrink-1 bg-light rounded py-2 px-3 mr-3" : "flex-shrink-1 bg-light rounded py-2 px-3 mr-3";

    let sender = document.createElement('div');
    sender.className = "font-weight-bold mb-1";
    sender.innerHTML = message.userName;
    let text = document.createElement('p');
    text.innerHTML = message.text;

    nameWithTxt.appendChild(sender);
    nameWithTxt.appendChild(text);

    container.appendChild(dateWithImage);
    container.appendChild(nameWithTxt);
    chat.appendChild(container);
}
