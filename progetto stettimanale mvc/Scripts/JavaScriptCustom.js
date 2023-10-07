$(document).ready(function () {
    $("#btnCerca").click(function () {
        $("#a").empty()
        let codicefiscale = $("#codicefiscale").val();

        $.ajax({
            method: 'POST',
            url: 'query1',
            data: { codicefiscale1: codicefiscale },

            success: function (data) {
                $.each(data, function (n, e) {
                    let prenotazione =
                        `
             <div class="d-flex"> <p class="me-1"> ${n + 1}  </p>  <p class="me-1"> numero prenotazione ${e.Idprenotazione}  </p> <p>costo camera ${e.Costotot} </p></div>

                    `

                    $("#a").append(prenotazione)
                })
            }
        })
    })
    $("#btnCerca2").click(function () {
        $("#a").empty()
        $.ajax({
            method: 'GET',
            url: 'query2',

            success: function (data) {
                $.each(data, function (n, e) {
                    let prenotazione =
                        `
                    <div class="d-flex"> <p class="me-1"> ${n + 1}  </p>  <p class="me-1"> numero prenotazione ${e.Idprenotazione}  </p> <p>costo camera ${e.Costotot} </p></div>

                    `

                    $("#a").append(prenotazione)
                })
            }
        })
    })
})