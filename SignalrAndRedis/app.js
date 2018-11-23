import * as tool from "./common.js";
import allPerson from "./allPerson.js";

(function() {
  $("#title").text(window.location.host);
  let $sendBtn = $("#send");
  let $sendPrivateBtn = $("#sendPrivateBtn");
  let $msgDom = $("#msg");
  let $room = $("#room");

  // get user id by hash ... for demo test
  let code = window.location.hash ? window.location.hash.substring(1) : 10005;
  let data = allPerson.find(x => x.code == code);

  // Data Binding to UI
  $("#name").val(data.name);
  $("#channel").text(data.channel.map(x => x.name).join("、"));
  // Generate People Options
  let optionHtml = "";
  for (let key in allPerson) {
    let obj = allPerson[key];
    optionHtml += `<option value=${obj.code}>${obj.name}</option>`;
  }
  document.getElementById("userId").innerHTML = optionHtml;

  // Generate Channel Options
  optionHtml = "";
  for (let key in data.channel) {
    let obj = data.channel[key];
    optionHtml += `<option value=${obj.code}>${obj.name}</option>`;
  }
  document.getElementById("channelId").innerHTML = optionHtml;

  // set client method
  for (let index = 0; index < data.channel.length; index++) {
    let currectChannelId = data.channel[index].id;
    let currectProxy = tool.getProxy(currectChannelId);
    currectProxy.client.received = msg => $room.append(`<li>${msg}</li>`);
  }

  $.connection.hub.qs = { id: data.code };
  $.connection.hub.start().done(function() {
    $sendBtn.on("click", function() {
      let currectProxy = tool.getProxy($("#channelId").val());
      currectProxy.server.send(
        data.name,
        `${$msgDom.val()} - ${window.location.host}`
      );
      $msgDom.val("");
    });

    $sendPrivateBtn.on("click", function() {
      // userId is a pk for user
      let userId = $("#userId").val();
      let currectProxy = tool.getProxy($("#channelId").val());
      currectProxy.server.sendPrivateMsg(
        userId,
        data.name,
        `${$msgDom.val()} - ${window.location.host}`
      );
      $msgDom.val("");
    });
  });
})();
