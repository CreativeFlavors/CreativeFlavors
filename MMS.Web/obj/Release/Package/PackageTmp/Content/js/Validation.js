//salesorder Validation
function quantityvalidate() {
    var number = $('#quantity').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#error1').text("Please Enter the Qty");

    }
    else if (filter.test(number)) {
        $('#error1').text("");
        $('#quantity').css('border', '#fff');
    }
    else {
        $('#quantity').css('border', '3px solid red');
        $('#error1').text("Please enter valid number");
    }
}
function disvalidate() {
    var number = $('#discountperid').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (filter.test(number) || number === "") {
        $('#error2').text("");
        $('#discountperid').css('border', '#fff');
    }
    else {
        $('#discountperid').css("border", "3px solid red");
        $('#error2').text("Please enter valid number");
    }
}
function BuyerNamefun() {
    var City = $('#BuyerName').val();
    if (City === "" || City === null) {
        $('#berror').text("Please select Buyer");
    } else {
        $('#berror').text("");
    }
}
function ProductNamefun() {
    var City = $('#ProductName').val();
    if (City === "" || City === null) {
        $('#perror').text("Please select Product");
    } else {
        $('#perror').text("");
    }
}
function order() {
    quantityvalidate();
    BuyerNamefun();
    ProductNamefun();
    disvalidate();
}
function conforder() {
    BuyerNamefun();
    ProductNamefun();
}
//Indent Validation
function matfun() {
    var City = $('#MaterialId').val();
    if (City === "" || City === null) {
        $('#materror').text("Please select Raw Material");
    } else {
        $('#materror').text("");

    }
}
function quantityINvalidate() {
    var number = $('#RequiredQty').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#inerror').text("Please Enter the Qty");
        $('#RequiredQty').css('border', '2px solid red');
    }
    else if (filter.test(number)) {
        $('#inerror').text("");
        $('#RequiredQty').css('border', '#fff');
    }
    else {
        $('#RequiredQty').css('border', '2px solid red');
        $('#inerror').text("Please enter valid number");
    }
}
function addindent() {
    quantityINvalidate();
    matfun();
}
//BOM Validation 
function quantitybom() {
    var number = $('#Requiredqty').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#bomerror').text("Please Enter the Qty");
    }
    else if (filter.test(number)) {
        $('#bomerror').text("");
    }
    else {
        $('#bomerror').text("Please enter valid number");
    }
}
function subquantitybom() {
    var number = $('#subRequiredqty').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#subbomerror').text("Please Enter the Qty");
    }
    else if (filter.test(number)) {
        $('#subbomerror').text("");
    }
    else {
        $('#subbomerror').text("Please enter valid number");
    }
}
function materialfun() {
    var City = $('#Productid').val();
    if (City === "" || City === null) {
        $('#materror').text("Please select Raw Material");
    } else {
        $('#materror').text("");
    }
}
function submaterialfun() {
    var City = $('#ProductSUBid').val();
    if (City === "" || City === null) {
        $('#suberror').text("Please select Sub Assembly Product");
    } else {
        $('#suberror').text("");

    }
}
function addbom() {
    quantitybom();
    materialfun();
}
function addsubbom() {
    subquantitybom();
    submaterialfun();
}
//GRN Validxation
function GRNUnitPrice() {
    var number = $('#UnitPrice').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        alert("Please Enter the UnitPrice");
    }
    else if (filter.test(number)) {
    }
    else {
        alert("Please enter valid number");

    }
}
function supplierfun() {
    var City = $('#SupplierId').val();
    if (City === "" || City === null) {
        $('#sperror').text("Please select Raw Material");
    } else {
        $('#sperror').text("");
    }
}
function ponofun() {
    var City = $('#PoHeaderId').val();
    if (City === "" || City === null) {
        $('#poheerror').text("Please select PONO");
    } else {
        $('#poheerror').text("");
    }
}
function Inreffun() {
    var City = $('#RefInvoiceNumber').val();
    if (City === "" || City === null) {
        $('#reinerror').text("Please Enter Ref-Invoice no ");
    } else {
        $('#reinerror').text("");
    }
}
function indatefun() {
    var City = $('#RefInvoiceDate').val();
    if (City === "" || City === null) {
        $('#indateerror').text("Please select Date");
    } else {
        $('#indateerror').text("");
    }
}


function confirsmGRN() {
    indatefun();
    Inreffun();
    ponofun();
    supplierfun();
}

function poinfun() {
    var City = $('#IndentNumber').val();
    if (City === "" || City === null) {
        $('#inerror').text("Please select Indent No ");
    } else {
        $('#inerror').text("");
    }
}
function suppofun() {
    var City = $('#SupplierId').val();
    if (City === "" || City === null) {
        $('#sperror').text("Please select Supplier Name");
    } else {
        $('#sperror').text("");
    }
}
function profun() {
    var City = $('#ProductId').val();
    if (City === "" || City === null) {
        $('#proerror').text("Please select Product Name ");
    } else {
        $('#proerror').text("");
    }
}

function pocon() {
    poinfun();
    suppofun();
    profun();
}
//product
function ProductTypes() {
    var Prod = $('#ProductType').val();
    if (Prod === "") {
        $('#error1').text("Please select Product Type ");
    } else {
        $('#error1').text("");
    }
}
function Category() {
    var City1 = $('#MaterialCategoryMasterId').val();
    if (City1 === "" || City1 === null) {
        $('#error2').text("Please select Category");
    } else {
        $('#error2').text("");
    }
} function ProdCod() {
    var City2 = $('#ProductCode').val();
    if (City2 === "" || City2 === null) {
        $('#error3').text("Please Enter Product Code");
    } else {
        $('#error3').text("");
    }
} function ProdName() {
    var City = $('#ProductName').val();
    if (City === "" || City === null) {
        $('#error4').text("Please Enter Product Name ");
    } else {
        $('#error4').text("");
    }
} function ProdDes() {
    var City = $('#ProductDesc').val();
    if (City === "" || City === null) {
        $('#error5').text("Please Enter Product Description ");
    } else {
        $('#error5').text("");
    }
}
function Costs() {
    var number = $('#cost').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#error6').text("Please Enter Cost");
    }
    else if (filter.test(number)) {
        $('#error6').text("");
    }
    else {
        $('#error6').text("Please enter valid number");

    }
} function Prices() {
    var number = $('#Price').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#error7').text("Please Enter Price");
    }
    else if (filter.test(number)) {
        $('#error7').text("");
    }
    else {
        $('#error7').text("Please enter valid number");

    }
} function proweights() {
    var number = $('#weight').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#error8').text("Please Enter the Weight");
    }
    else if (filter.test(number)) {
        $('#error8').text("");
    }
    else {
        $('#error8').text("Please enter valid number");

    }
}
function Uom() {
    var City = $('#UomMasterId').val();
    if (City === "" || City === null) {
        $('#error9').text("Please Select UOM");
    } else {
        $('#error9').text("");
    }
} function Tax() {
    var City = $('#TaxMasterId').val();
    if (City === "" || City === null) {
        $('#error10').text("Please Select Tax Type");
    } else {
        $('#error10').text("");
    }
} function Store() {
    var City = $('#StoreId').val();
    if (City === "" || City === null) {
        $('#error12').text("Please Select Store Name");
    } else {
        $('#error12').text("");
    }
}
function productions() {
    var City = $('#productionperday').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (City === "" || City === null) {
        $('#error13').text("Please Enter Production Per-day");
    }
    else if (filter.test(City)) {
        $('#error13').text("");
    } else
    {
        $('#error13').text("Please enter valid number");
    }
}
function MaxSt() {
    var number = $('#MaxStock').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;

    if (number === "") {
        $('#error15').text("Please Enter Max Stock");
    } else if (filter.test(number)) {
        var numericValue = parseFloat(number);

        if (numericValue >= 1) {
            $('#error15').text("");
        } else {
            $('#error15').text("At least max stock starts from 1");
        }
    } else {
        $('#error15').text("Please enter a valid number");
    }
}

function MinSt() {
    var number = $('#MinStock').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#error14').text("Please Enter Min Stock");
    }
    else if (filter.test(number)) {
        $('#error14').text("");
    }
    else {
        $('#error14').text("Please enter valid number");

    }
}


function productfun() {
    MinSt();
    MaxSt();
    productions();
    Store();
    Tax();
    Uom();
    proweights();
    ProdDes();
    Costs();
    Prices();
    ProductTypes();
    Category();
    ProdCod();
    ProdName();
}


// Account Receivable

function Cashfun() {
    var number = $('#Cash').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#cerror').text("Please Enter Cash");
    }
    else if (filter.test(number)) {
        $('#cerror').text("");
    }
    else {
        $('#cerror').text("Please enter valid number");

    }
}
function Debitnotedatefun() {
    var number = $('#Debitnotedate').val();
    if (number == "") {
        $('#Debitdateerror').text("Please Click Debitnotedate");
    }
    else {
        $('#Debitdateerror').text("");

    }
} function Debitnotereffun() {
    var number = $('#Debitnoteref').val();
    if (number == "") {
        $('#debitnoteerror').text("Please Enter Ref no");
    }
    else {
        $('#debitnoteerror').text("");

    }
}
function Debitnotevaluefun() {
    var number = $('#Debitnotevalue').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#Debitvalerror').text("Please Enter Amount");
    }
    else if (filter.test(number)) {
        $('#Debitvalerror').text("");
    }
    else {
        $('#Debitvalerror').text("Please enter valid number");

    }
}
function payfun() {
    var number = $('#PaymentAmount').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#payerror').text("Please Enter Amount");
    }
    else if (filter.test(number)) {
        $('#payerror').text("");
    }
    else {
        $('#payerror').text("Please enter valid number");

    }
}
function payreffun() {
    var number = $('#PaymentRefNo').val();
    if (number == "") {
        $('#payreferror').text("Please Enter Ref no");
    }
    else {
        $('#payreferror').text("");
    }
}

function debitreffun() {
    Debitnotedatefun();
    Debitnotereffun();
    Debitnotevaluefun();
}
function paysfun() {
    payfun();
    payreffun(); 
}


// account payable

function PAYCashfun() {
    var number = $('#Cash').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#pcerror').text("Please Enter Cash");
    }
    else if (filter.test(number)) {
        $('#pcerror').text("");
    }
    else {
        $('#pcerror').text("Please enter valid number");

    }
}
function creditnotedatefun() {
    var number = $('#CreditNoteDate').val();
    if (number == "") {
        $('#Creditdateerror').text("Please Click  Creditnotedate");
    }
    else {
        $('#Creditdateerror').text("");

    }
}
function creditnotereffun() {
    var number = $('#CreditNoteRefNo').val();
    if (number == "") {
        $('#Creditnoteerror').text("Please Enter Ref no");
    }
    else {
        $('#Creditnoteerror').text("");
    }
}
function creditnotevaluefun() {
    var number = $('#CreditNoteValue').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#Creditvalerror').text("Please Enter Amount");
    }
    else if (filter.test(number)) {
        $('#Creditvalerror').text("");
    }
    else {
        $('#Creditvalerror').text("Please enter valid number");

    }
}
function Payablepayfun() {
    var number = $('#PaymentAmount').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#Payablepayerror').text("Please Enter Amount");
    }
    else if (filter.test(number)) {
        $('#Payablepayerror').text("");
    }
    else {
        $('#Payablepayerror').text("Please enter valid number");

    }
}
function Payablepayreffun() {
    var number = $('#PaymentRefNo').val();
    if (number == "") {
        $('#Payablepayreferror').text("Please Enter Ref no");
    }
    else {
        $('#Payablepayreferror').text("");
    }
}

function Payablecreditfun() {
    creditnotedatefun();
    creditnotereffun();
    creditnotevaluefun();
}
function Payablepaysfun() {
    Payablepayfun();
    Payablepayreffun();
}

function ProductionQtyfun() {
    var number = $('#ProductionQty').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#proderror').text("Please Enter ProductionQty");
    }
    else if (filter.test(number)) {
        $('#proderror').text("");
    }
    else {
        $('#proderror').text("Please enter valid number");

    }
}

function prodproductfun() {
    var City = $('#ProductId').val();
    if (City === "" || City === null) {
        $('#prodproerror').text("Please select Product");
    } else {
        $('#prodproerror').text("");
    }
}
function prodproductsubfun() {
    var City = $('#ProductSubId').val();
    if (City === "" || City === null) {
        $('#prodsuberror').text("Please select Sub_Product");
    } else {
        $('#prodsuberror').text("");
    }
}
function storefun() {
    var City = $('#StoreCode').val();
    if (City === "" || City === null) {
        $('#storeerror').text("Please select Store");
    } else {
        $('#storeerror').text("");
    }
}

function savevalidate() {
    prodproductfun();
    ProductionQtyfun();
    storefun();
}
function subvalidate() {
    prodproductsubfun();
    ProductionQtyfun();
    storefun();
}

// supplier material

function supplfun() {
    var City = $('#SupplierId').val();
    if (City === "" || City === null) {
        $('#serror').text("Please select Supplier");
    } else {
        $('#serror').text("");
    }
} function Categoryfun() {
    var City = $('#category').val();
    if (City === "" || City === null) {
        $('#cerror').text("Please select category");
    } else {
        $('#cerror').text("");
    }
} function Producfun() {
    var City = $('#ProductId').val();
    if (City === "" || City === null) {
        $('#perror').text("Please select Product");
    } else {
        $('#perror').text("");
    }
} function UomIdfun() {
    var City = $('#UomId').val();
    if (City === "" || City === null) {
        $('#uomerror').text("Please select UOM");
    } else {
        $('#uomerror').text("");
    }
} function TaxIdfun() {
    var City = $('#TaxId').val();
    if (City === "" || City === null) {
        $('#taxerror').text("Please select Tax");
    } else {
        $('#taxerror').text("");
    }
}


function savesupplierfun() {
    TaxIdfun();
    UomIdfun();
    Producfun();
    Categoryfun();
    supplfun();
}



// customer details


function BuyerIdfun() {
    var City = $('#BuyerId').val();
    if (City === "" || City === null) {
        $('#BuyerIderror').text("Please select Buyer Name");
    } else {
        $('#BuyerIderror').text("");
    }
}
function IsDefaultfun() {
    var City = $('#IsDefault').val();
    if (City === "" || City === null) {
        $('#IsDefaulterror').text("Please select Type of Address");
    } else {
        $('#IsDefaulterror').text("");
    }
}
function AddressTypefun() {
    var City = $('#AddressType').val();
    if (City === "" || City === null) {
        $('#AddressTypeerror').text("Please select AddressType");
    } else {
        $('#AddressTypeerror').text("");
    }
}
function Add1fun() {
    var City = $('#Add1').val();
    if (City === "" || City === null) {
        $('#Add1error').text("Please Enter Address");
    } else {
        $('#Add1error').text("");
    }
}
function Add2fun() {
    var City = $('#Add2').val();
    if (City === "" || City === null) {
        $('#Add2error').text("Please Enter Address");
    } else {
        $('#Add2error').text("");
    }
}
function Add3fun() {
    var City = $('#Add3').val();
    if (City === "" || City === null) {
        $('#Add3error').text("Please Enter Address");
    } else {
        $('#Add3error').text("");
    }
}
function Countryfun() {
    var City = $('#Country').val();
    if (City === "" || City === null) {
        $('#Countryerror').text("Please select Country");
    } else {
        $('#Countryerror').text("");
    }
}
function Statefun() {
    var City = $('#State').val();
    if (City === "" || City === null) {
        $('#Stateerror').text("Please select State");
    } else {
        $('#Stateerror').text("");
    }
}
function Cityfun() {
    var City = $('#City').val();
    if (City === "" || City === null) {
        $('#Cityerror').text("Please select City");
    } else {
        $('#Cityerror').text("");
    }
} function ZipCodefun() {
    var Mobilenumber = $('#ZipCode').val();
    var filter = /^[0-9]{6}$/;
    if (Mobilenumber == "") {
        $('#Ziperror').text("Please enter the number");
    }
    else if (filter.test(Mobilenumber)) {
        $('#Ziperror').text("");
    }
    else {
        $('#Ziperror').text("Please enter valid number");
    }
} function ContactNamefun() {
    var City = $('#ContactName').val();
    var phoneregx = /^[A-Za-z]*$/;
    if (City === "" || City === null) {
        $('#ContactNameerror').text("Please select Contact Name");
    } else if (phoneregx.test(City)) {
        $('#ContactNameerror').text("");
    } else {
        $('#ContactNameerror').text("Please enter valid Name");
    }
}
function Notesfun() {
    var City = $('#Notes').val();
    if (City === "" || City === null) {
        $('#Noteserror').text("Please select City");
    } else {
        $('#Noteserror').text("");
    }
}
function numbervalidatefun() {

    var Mobilenumber = $('#Phone').val();

    var filter = /^[0-9]{10}$/;
    if (Mobilenumber == "") {
        $('#Pherror').text("Please enter the number");
    }
    else if (filter.test(Mobilenumber)) {
        $('#Pherror').text("");
    }
    else {
        $('#Pherror').text("Please enter valid number");
    }
}
function emailfun() {

    var email = $('#Email').val();
    var phoneregx = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    if (email == "") {
        $('#emailerror').text("Please enter the email");
    }
    else if (phoneregx.test(email)) {
        $('#emailerror').text("");
    }
    else {
        $('#emailerror').text("Please enter valid Email");
    }
}
function savecustomerdetails() {
    Notesfun();
    emailfun();
    Cityfun();
    ContactNamefun();
    ZipCodefun();
    Statefun();
    Countryfun();
    numbervalidatefun();
    Add3fun();
    Add1fun(); Add2fun();
    BuyerIdfun();
    IsDefaultfun();
    AddressTypefun();

}
