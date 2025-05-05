const today = new Date();

$('#anoVendedor').datepicker({
	format: "yyyy",
	viewMode: "years",
	minViewMode: "years",
	autoclose: true
});

$("#mesVendedor").val(today.getMonth() + 1);

$('#anoVendedor').val(today.getFullYear());


$("#LojaId").attr("disabled", true);