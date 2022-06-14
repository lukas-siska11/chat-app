document.getElementById("sendButton").addEventListener("click", function (event) {
    let messageInput = document.getElementById("messageInput");
    let message = messageInput.value;
    let chatRoomId = $('#chat-room-messages').data('id');
    
    messageInput.value = null;
    connection.invoke("SendMessage", chatRoomId, message).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});

document.getElementById("messageInput").addEventListener("keyup", function (event) {
    let chatRoomId = $('#chat-room-messages').data('id');
    connection.invoke("SendIsTypingMessage", chatRoomId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});