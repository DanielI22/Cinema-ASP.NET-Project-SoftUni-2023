function handleSeatSelection() {
    var seatId = $(this).data('seat-id');
    var reserved = $(this).hasClass('reserved');

    if (!reserved) {
        if (selectedSeats.includes(seatId)) {
            selectedSeats = selectedSeats.filter(id => id !== seatId);
        } else if (selectedSeats.length < 3) {
            selectedSeats.push(seatId);
        }
        else {
            $('#customAlert').show();
            $('#customAlert').focus();
            $('#customAlert').delay(3000).fadeOut();
        }
    }

    if (selectedSeats.length > 0) {
        $('#finishReservation').removeAttr('disabled');
    } else {
        $('#finishReservation').attr('disabled', true);
    }

    updateSeatColors();
}

function updateSeatColors() {
    $('.seat').each(function () {
        var seatId = $(this).data('seat-id');
        if (selectedSeats.includes(seatId)) {
            $(this).removeClass('reserved').addClass('selected');
        } else if ($(this).hasClass('reserved')) {
            $(this).removeClass('selected');
        } else {
            $(this).removeClass('selected');
        }
    });
}

function finishReservation() {
    $("#selectedSeats").val(selectedSeats.join(','));
    $("#reservationForm").submit();
}
