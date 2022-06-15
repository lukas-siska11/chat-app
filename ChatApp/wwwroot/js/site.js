let connection = new signalR.HubConnectionBuilder().withUrl("/chat-hub").build();
connection.start();

connection.on("NewMessage", function (messageId) {
    $.ajax({
        type: 'GET',
        url: 'Detail?handler=Message',
        data: {
            messageId: messageId
        },
        success: function (html) {
            let noMessageText = document.getElementById('noMessagesText');
            if (typeof noMessageText !== 'undefined' && noMessageText !== null) {
                noMessageText.remove();
            }

            $(document).find('#messageList').append($(html));

            let chatRoomBody = document.getElementById('chat-room-body');
            chatRoomBody.scrollTop = chatRoomBody.scrollHeight;
        },
        error: function (error) {
            console.error(error);
        }
    });
});

let isTypingMessageTimerActive = false;

connection.on("IsTypingMessage", function (chatRoomId, userDataAsJson) {
    let userData = JSON.parse(userDataAsJson);
    let chatRoomMessages = $('#chat-room-messages');
    let currentChatRoomId = chatRoomMessages.data('id');
    let currentUserId = chatRoomMessages.data('user-id');
    
    if (currentChatRoomId !== chatRoomId || userData.UserId === currentUserId || isTypingMessageTimerActive) {
        return;
    }
    
    let isTypingText = $(document).find('#isTypingText');

    isTypingText.text(userData.UserFullName + ' is typing...');

    isTypingMessageTimerActive = true;
    setTimeout(function () {
        isTypingText.html('&#8205;');
        isTypingMessageTimerActive = false;
    }, 3000);
});

$(document).ready(() => {
    $(document).on('click', '.chat-room-card', showChatRoomMessages);
});

function showChatRoomMessages(e) {
    e.preventDefault();

    let chatRoomId = $(this).data('id');
    $.ajax({
        type: 'GET',
        url: 'Detail',
        data: {
          chatRoomId: chatRoomId
        },
        success: function (html) {
            $(document).find('#chat-room-messages').replaceWith($(html));
            let chatRoomBody = document.getElementById('chat-room-body');
            chatRoomBody.scrollTop = chatRoomBody.scrollHeight;
        },
        error: function (error) {
            console.error(error);
        }
    });
}
