
let lineNo = 1;
$(document).ready(function () {
$(".add-row").click(function () {
    markup = "<tr><td>This is row "
        + lineNo + "</td></tr>";
    tableBody = $("table tbody");
    tableBody.append(markup);
    lineNo++;
});
});