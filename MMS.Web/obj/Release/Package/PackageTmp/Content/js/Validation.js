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
}
function accountypefun() {
    var City = $('#AccountTypeId').val();
    if (City === "" || City === null) {
        $('#AccountTypResult').text("Please select AccountType");
    } else {
        $('#AccountTypResult').text("");
    }
}
function Categoryfun() {
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
function SupplierIdfun() {
    var City = $('#SupplierId').val();
    if (City === "" || City === null) {
        $('#suppIderror').text("Please select Supllier Name");
    } else {
        $('#suppIderror').text("");
    }
}
function IsDefaultfun() {
    var City = $('#addressvarient').val();
    if (City === "" || City === null) {
        $('#IsDefaulterror').text("Please select Type of Address");
    } else {
        $('#IsDefaulterror').text("");
    }
}
function AddressTypefun() {
    var City = $('#AddressType').val();
    var addressvarient = $("#addressvarient").val();
    if (addressvarient != 0 || addressvarient == "") {
        if (City === "" || City === null) {
            $('#AddressTypeerror').text("Please select AddressType");
        } else {
            $('#AddressTypeerror').text("");
        }
    }
   
}
function Add1fun() {
    var City = $('#Add1').val();
    if (City === "" || City === null) {
        $('#Add1error').text("Please enter an address.");
    } else if (City.length > 100) {
        $('#Add1error').text("Address must be 100 characters or less.");
    } else  {
        $('#Add1error').text("");
    }
}
function Add2fun() {
    var City = $('#Add2').val();
    if (City === "" || City === null) {
        $('#Add2error').text("Please enter an address.");
    } else if (City.length > 100) {
        $('#Add2error').text("Address must be 100 characters or less.");
    } else  {
        $('#Add2error').text("");
    }
}
function Add3fun() {
    var City = $('#Add3').val();
    if (City === "" || City === null) {
        $('#Add3error').text("Please enter an address.");
    } else if (City.length > 100) {
        $('#Add3error').text("Address must be 100 characters or less.");
    } else  {
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
    var phoneregx = /^[a-zA-Z\s\.]*$/;
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
        $('#Noteserror').text("Please Enter Notes");
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
    vatnumfun();
    Add1fun(); Add2fun();
    BuyerIdfun();
    IsDefaultfun();
    SupplierIdfun();
    AddressTypefun();


}



//Supplier Screen


function Suppliernamefun() {
    var City = $('#Suppliername').val();
    var phoneregx = /^[a-zA-Z\s\.]*$/;
    if (City === "" || City === null) {
        $('#SuppliernameResult').text("Please Enter Supplier Name");
    } else if (phoneregx.test(City)) {
        $('#SuppliernameResult').text("");
    } else {
        $('#SuppliernameResult').text("Please enter valid Name");
    }
}
function SupplierCodefun() {
    var City = $('#suppliercode').val();
    if (City === "" || City === null) {
        $('#suppCodetResult').text("Please select Supplier Code");
    } else {
        $('#suppCodetResult').text("");
    }
} function Accountfun() {
    var z1 = /^[0-9]*$/;
    var City = $('#Account').val();
    if (City === "" || City === null) {
        $('#AccountResult').text("Please select Account Number");
    } else if (z1.test(City)) {
        $('#AccountResult').text("");
    } else {
        $('#AccountResult').text("Please Enter Valid Number");
    }
} function AccountNamefun() {
    var phoneregx = /^[a-zA-Z\s\.]*$/;
    var City = $('#AccountName').val();
    if (City === "" || City === null) {
        $('#AccountNameResult').text("Please select Account Name");
    } else if (phoneregx.test(City)) {
        $('#AccountNameResult').text("");
    } else {
        $('#AccountNameResult').text("Please enter valid Name");
    }
}
function AccountDescriptionfun() {
    var City = $('#AccountDescription').val();
    if (City === "" || City === null) {
        $('#AccountDescriptionNameResult').text("Please Enter Account Description");
    } else {
        $('#AccountDescriptionNameResult').text("");
    }
}
function EmailContactfun() {

    var email = $('#EmailContact').val();
    var phoneregx = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    if (email == "") {
        $('#EmailResult').text("Please enter the Email Contact");
    }
    else if (phoneregx.test(email)) {
        $('#EmailResult').text("");
    }
    else {
        $('#EmailResult').text("Please enter valid Email");
    }
}
function Physical2fun() {
    var City = $('#Physical2').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Physical2Reult').text("Please enter an Physical Address 2.");
    } else if (City.length > 100) {
        $('#Physical2Reult').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Physical2Reult').text("");
    }
} function Physical5fun() {
    var City = $('#Physical5').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Physical5Result').text("Please enter an Physical Address 5.");
    } else if (City.length > 100) {
        $('#Physical5Result').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Physical5Result').text("");
    }
} function Physical4fun() {
    var City = $('#Physical4').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Physical4Result').text("Please enter an Physical Address 4.");
    } else if (City.length > 100) {
        $('#Physical4Result').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Physical4Result').text("");
    }
} function Physical3fun() {
    var City = $('#Physical3').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Physical3Result').text("Please enter an Physical Address 3.");
    } else if (City.length > 100) {
        $('#Physical3Result').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Physical3Result').text("");
    }
} function Physical1fun() {
    var City = $('#Physical1').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Physical1Result').text("Please enter an Physical Address 1.");
    } else if (City.length > 100) {
        $('#Physical1Result').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Physical1Result').text("");
    }
}
function PhysicalCodefun() {
    var Mobilenumber = $('#PhysicalCode').val();
    var filter = /^[0-9]{6}$/;
    if (Mobilenumber == "") {
        $('#PhysicalCodeResult').text("Please Enter the Physical Code");
    }
    else if (filter.test(Mobilenumber)) {
        $('#PhysicalCodeResult').text("");
    }
    else {
        $('#PhysicalCodeResult').text("Please Enter valid number");
    }
}

function Postal1fun() {
    var City = $('#Postal1').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Postal1Result').text("Please enter an Postal Address 1.");
    } else if (City.length > 100) {
        $('#Postal1Result').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Postal1Result').text("");
    }
}
function Postal2fun() {
    var City = $('#Postal2').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Postal2Result').text("Please enter an Postal Address 2.");
    } else if (City.length > 100) {
        $('#Postal2Result').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Postal2Result').text("");
    }
}
function Postal3fun() {
    var City = $('#Postal3').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Postal3Result').text("Please enter an Postal Address 3.");
    } else if (City.length > 100) {
        $('#Postal3Result').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Postal3Result').text("");
    }
}
function Postal4fun() {
    var City = $('#Postal4').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Postal4Result').text("Please enter an Postal Address 4.");
    } else if (City.length > 100) {
        $('#Postal4Result').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Postal4Result').text("");
    }
}
function Postal5fun() {
    var City = $('#Postal5').val();
    var addressRegx = /^[a-zA-Z\s\/]*$/;
    if (City === "" || City === null) {
        $('#Postal5Result').text("Please enter an Postal Address 4.");
    } else if (City.length > 100) {
        $('#Postal5Result').text("Address must be 100 characters or less.");
    } else if (addressRegx.test(City)) {
        $('#Postal5Result').text("");
    }
}
function PostalCodefun() {
    var Mobilenumber = $('#PostalCode').val();
    var filter = /^[0-9]{6}$/;
    if (Mobilenumber == "") {
        $('#PostalCodeResult').text("Please Enter the Postal Code");
    }
    else if (filter.test(Mobilenumber)) {
        $('#PostalCodeResult').text("");
    }
    else {
        $('#PostalCodeResult').text("Please Enter valid number");
    }
}
function Telephone1fun() {
    var Mobilenumber = $('#Telephone1').val();
    var filter = /(?:\(?\+\d{2}\)?\s*)?\d+(?:[ -]*\d+)*$/;
    if (Mobilenumber == "") {
        $('#Telephone1Result').text("Please Enter the Telephone1");
    }
    else if (filter.test(Mobilenumber)) {
        $('#Telephone1Result').text("");
    }
    else {
        $('#Telephone1Result').text("Please Enter valid number");
    }
}
function Telephone2fun() {
    var Mobilenumber = $('#Telephone2').val();
    var filter = /(?:\(?\+\d{2}\)?\s*)?\d+(?:[ -]*\d+)*$/;
    if (Mobilenumber == "") {
        $('#Telephone2Result').text("Please Enter the Telephone2");
    }
    else if (filter.test(Mobilenumber)) {
        $('#Telephone2Result').text("");
    }
    else {
        $('#Telephone2Result').text("Please Enter valid number");
    }
}
function EmailAccountsfun() {

    var email = $('#EmailAccounts').val();
    var phoneregx = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    if (email == "") {
        $('#EmailAccountsResult').text("Please enter the Email Account");
    }
    else if (phoneregx.test(email)) {
        $('#EmailAccountsResult').text("");
    }
    else {
        $('#EmailAccountsResult').text("Please enter valid Email");
    }
} function EmailEmergencyfun() {

    var email = $('#EmailEmergency').val();
    var phoneregx = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    if (email == "") {
        $('#EmergencyNameResult').text("Please enter the Emergency Email");
    }
    else if (phoneregx.test(email)) {
        $('#EmergencyNameResult').text("");
    }
    else {
        $('#EmergencyNameResult').text("Please enter valid Email");
    }
}
function VatNumberfun() {
    var City = $('#VatNumber').val();
    if (City === "" || City === null) {
        $('#VatResult').text("Please Enter VatNumber");
    } else {
        $('#VatResult').text("");
    }
} function RegNumberfun() {
    var City = $('#RegNumber').val();
    if (City === "" || City === null) {
        $('#RegResult').text("Please Enter RegNumber");
    } else {
        $('#RegResult').text("");
    }
}
function DcBalancebom() {
    var number = $('#DcBalance').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#DcBalanceNameResult').text("Please Enter the DcBalance");
    }
    else if (filter.test(number)) {
        $('#DcBalanceNameResult').text("");
    }
    else {
        $('#DcBalanceNameResult').text("Please enter valid number");
    }
}
function CreditLimitfun() {
    var number = $('#CreditLimit').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#CreditResult').text("Please Enter the CreditLimit");
    }
    else if (filter.test(number)) {
        $('#CreditResult').text("");
    }
    else {
        $('#CreditResult').text("Please enter valid number");
    }
} function Interestfun() {
    var number = $('#Interest').val();
    var filter = /^[0-9]+(\.[0-9]+)?$/;
    if (number == "") {
        $('#inResult').text("Please Enter the Interest");
    }
    else if (filter.test(number)) {
        $('#inResult').text("");
    }
    else {
        $('#inResult').text("Please enter valid number");
    }
}

function TaxTypeIdfun() {
    var City = $('#TaxTypeId').val();
    if (City === "" || City === null) {
        $('#TaxTypeIdResult').text("Please Enter TaxTypeId");
    } else {
        $('#TaxTypeIdResult').text("");
    }
}
function vatnumfun() {
    var City = $('#vatnumber').val();
    if (City === "" || City === null) {
        $('#vaterror').text("Please Enter VAT Number");
    } else {
        $('#vaterror').text("");
    }
}
function CurrencyIDfun() {
    var City = $('#CurrencyID').val();
    if (City === "" || City === null) {
        $('#CurrencyIDResult').text("Please Enter Currency");
    } else {
        $('#CurrencyIDResult').text("");
    }
} function ForeignCurrencyfun() {
    var City = $('#ForeignCurrency').val();
    if (City === "" || City === null) {
        $('#ForeignCurrencyNameResult').text("Please Enter Foreign_Currency");
    } else {
        $('#ForeignCurrencyNameResult').text("");
    }
}

function ChargeinResultfun() {

    var s = $('input[name="yes"]:checked').val();
    if (s != undefined) {
        $('#ChargeinResult').text("");
    } else {
        $('#ChargeinResult').text("Please Click the Interest Need or Not");
    }
}

function confirmsupplier() {
    ChargeinResultfun();
    Suppliernamefun();
    SupplierCodefun();
    Accountfun();
    AccountNamefun();
    AccountDescriptionfun();
    EmailContactfun();
    Physical1fun();
    PhysicalCodefun();
    accountypefun();
    Telephone1fun();
    Telephone2fun();
    EmailAccountsfun();
    EmailEmergencyfun();
    VatNumberfun();
    RegNumberfun();
    DcBalancebom();
    CreditLimitfun();
    Interestfun();
    TaxTypeIdfun();
    CurrencyIDfun();
    ForeignCurrencyfun();
}

function loginEmailfun() {
    var email = $('#Email').val();
    var phoneregx = /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|.(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;

    if (email == "") {
        $('#Emailpass').text("Please enter the Email");
    }
    else if (phoneregx.test(email)) {
        $('#Emailpass').text("");
    }
    else {
        $('#Emailpass').text("Please enter valid Email");
    }
}

function passwordfun() {

    var Password = $('#Password').val();
    var phoneregx = /^(?=.*[0-5])(?=.*[a-z])(?=.*[A-Z])(?=.*\W)(?!.* ).{8,16}$/;

    if (Password == "") {
        $('#passerror').text("Please fill the password");
    }
    else if (phoneregx.test(Password)) {
        $('#passerror').text("");
    }
    else {
        $('#passerror').text("Please enter a valid Passsword");
    }
}

function confirmlogin() { 
    passwordfun();
    loginEmailfun();
}







////form validation for email


const validateEmail = (email) => {
    return email.match(
        /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/
    )
};


const validate = () => {
    console.log("Email called");
    //alert("validationjs email");

    const emailresult = document.getElementById("EmailResult");

    const emailcontact = document.getElementById("EmailContact");
    const email = emailcontact.value;
    console.log(email);
    emailresult.textContent = '';

    if (!validateEmail(email)) {
        emailresult.textContent = email + ' is invalid';
        if (email.trim() === '') {
            emailresult.textContent = '';
        }
        emailresult.style.color = 'red';
        emailresult.style.display = 'block';
        return false;
    }
    return true;
}


$(document).on('keyup', '#EmailContact', validate);
//document.getElementById("EmailContact").addEventListener('keyup', validate);

//textbox buyername validation

const validateBuyerName = (name) => {
    return name.match(/^[a-zA-Z0-9]+$/);
};

const validateName = () => {
    console.log("buyername called");


    const nameResult = document.getElementById("NameResult");
    const buyername = document.getElementById("BuyerShortName");
    const name = buyername.value;
    nameResult.textContent = '';

    if (!validateBuyerName(name)) {
        nameResult.textContent = name + 'is invalid';
        if (name.trim() === '') {
            nameResult.textContent = '';
        }
        nameResult.style.color = 'red';
        nameResult.style.display = 'block';
        return false;
    }

    return true;

}
$(document).on('keyup', '#BuyerShortName', validateName);

//var buyerShortName_element = document.getElementById("BuyerShortName");

//if (buyerShortName_element !== null) {
//    buyerShortName_element.addEventListener('keyup', validateName)
//}

//document.getElementById("BuyerShortName").addEventListener('keyup', validateName);


//buyercide validation

const validateBuyerCode = (buyercode) => {
    return buyercode.match(/^[a-zA-Z0-9]+$/);
};

const ValidateBuyerCode = () => {
    const buyerCodeResult = document.getElementById("BuyerCodeResult");
    const buyercodevalue = document.getElementById("BuyerCode");
    const buyercode = buyercodevalue.value;
    buyerCodeResult.textContent = '';

    if (!validateBuyerCode(buyercode)) {
        buyerCodeResult.textContent = buyercode + 'is invalid';
        if (buyercode.trim() === '') {
            buyerCodeResult.textContent = '';
        }
        buyerCodeResult.style.color = 'red';
        buyerCodeResult.style.display = 'block';
        return false;
    }

    return true;
}

$(document).on('keyup', '#BuyerCode', ValidateBuyerCode);
//document.getElementById("BuyerCode").addEventListener('keyup', ValidateBuyerCode);



//customername validation

const validateCustomerName = (customerName) => {
    return customerName.match(/^[a-zA-Z\s]+$/);
}

const ValidateCustomerName = () => {
    const customernameResult = document.getElementById("CustomerNameResult");
    const customernamevalue = document.getElementById("CustomerName");
    const customerName = customernamevalue.value;

    customernameResult.textContent = '';
    if (!validateCustomerName(customerName)) {
        customernameResult.textContent = customerName + 'is invalid';
        if (customerName.trim() === '') {
            customernameResult.textContent = '';
        }
        customernameResult.style.color = 'red';
        customernameResult.style.display = 'block';
        return false;
    }
    return true;


}
$(document).on('keyup', '#CustomerName', ValidateCustomerName);
/*document.getElementById("CustomerName").addEventListener('keyup', ValidateCustomerName);*/


//Account validation

const ValidateAccount = (account) => {
    return account.match(/^[a-zA-Z0-9]+$/);
}

const _ValidateAccount = () => {
    const accountresult = document.getElementById("AccountResult");
    const accountresultvalue = document.getElementById("Account");
    const account = accountresultvalue.value;

    accountresult.textContent = '';
    if (!ValidateAccount(account)) {
        accountresult.textContent = account + 'is invalid';
        if (account.trim() === '') {
            accountresult.textContent = '';
        }
        accountresult.style.color = 'red';
        accountresult.style.display = 'block';
        return false;
    }
    return true;
}
$(document).on('keyup', '#Account', _ValidateAccount);
//document.getElementById("Account").addEventListener('keyup', _ValidateAccount);

//Accountnmae validation

const ValidateAccountName = (accountname) => {
    return accountname.match(/^[a-zA-Z]+$/);
}

const _ValidateAccountName = () => {
    const accountnameresult = document.getElementById("AccountNameResult");
    const accountnameresultvalue = document.getElementById("AccountName");
    const accountname = accountnameresultvalue.value;

    accountnameresult.textContent = '';
    if (!ValidateAccountName(accountname)) {
        accountnameresult.textContent = accountname + 'is invalid';
        if (accountname.trim() === '') {
            accountnameresult.textContent = '';
        }
        accountnameresult.style.color = 'red';
        accountnameresult.style.display = 'block';
        return false;
    }
    return true;
}
$(document).on('keyup', '#AccountName', _ValidateAccountName);
//document.getElementById("AccountName").addEventListener('keyup', _ValidateAccountName);


//Accountdescription validation

const ValidateAccountDescription = (accountdescription) => {
    return accountdescription.match(/^[a-zA-Z\s]+$/);
}

const _ValidateAccountDescription = () => {
    const accountdescriptionresult = document.getElementById("AccountDescriptionResult");
    const accountdescriptionresultvalue = document.getElementById("AccountDescription");
    const accountdescription = accountdescriptionresultvalue.value;

    accountdescriptionresult.textContent = '';
    if (!ValidateAccountDescription(accountdescription)) {
        accountdescriptionresult.textContent = accountdescription + 'is invalid';
        if (accountdescription.trim() === '') {
            accountdescriptionresult.textContent = '';
        }
        accountdescriptionresult.style.color = 'red';
        accountdescriptionresult.style.display = 'block';

        return false;
    }
    return true;

}
$(document).on('keyup', '#AccountDescription', _ValidateAccountDescription);
//document.getElementById("AccountDescription").addEventListener('keyup', _ValidateAccountDescription);


//swiftcode validation

const ValidateSwiftCode = (swiftcode) => {
    return swiftcode.match(/^[0-9]{6}$/);
}

const _ValidateSwiftCode = () => {
    const swiftcoderesult = document.getElementById("SwiftCodeResult");
    const swiftcoderesultvalue = document.getElementById("SwiftCode");
    const swiftcode = swiftcoderesultvalue.value;

    swiftcoderesult.textContent = '';
    if (!ValidateSwiftCode(swiftcode)) {
        swiftcoderesult.textContent = swiftcode + 'is invalid';
        if (swiftcode.trim() === '') {
            swiftcoderesult.textContent = '';
        }
        swiftcoderesult.style.color = 'red';
        swiftcoderesult.style.display = 'block';

        return false;
    }
    return true;

}
$(document).on('keyup', '#SwiftCode', _ValidateSwiftCode);
//document.getElementById("SwiftCode").addEventListener('keyup', _ValidateSwiftCode);







//physical code

const ValidatePhysicalCode = (physicalcode) => {
    return physicalcode.match(/^[0-9]{6}$/);
}

const _ValidatePhysicalCode = () => {
    const physicalcoderesult = document.getElementById("PhysicalCodeResult");
    const physicalcoderesultvalue = document.getElementById("PhysicalCode");
    const physicalcode = physicalcoderesultvalue.value;

    physicalcoderesult.textContent = '';
    if (!ValidateSwiftCode(physicalcode)) {
        physicalcoderesult.textContent = physicalcode + 'is invalid';
        if (physicalcode.trim() === '') {
            physicalcoderesult.textContent = '';
        }
        physicalcoderesult.style.color = 'red';
        physicalcoderesult.style.display = 'block';
        return false;
    }
    return true;
}
$(document).on('keyup', '#PhysicalCode', _ValidatePhysicalCode);
//document.getElementById("PhysicalCode").addEventListener('keyup', _ValidatePhysicalCode);


//telephonmet 1 validation
const ValidateTelephone1 = (telephone1) => {
    return telephone1.match(/^[0-9]{10}$/);
}


const _ValidateTelephone1 = () => {
    const telephone1result = document.getElementById("Telephone1Result");
    const telephone1reusltvalue = document.getElementById("Telephone1");
    const telephone1 = telephone1reusltvalue.value;

    telephone1result.textContent = '';
    if (!ValidateTelephone1(telephone1)) {
        telephone1result.textContent = telephone1 + 'is invalid';
        if (telephone1.trim() === '') {
            telephone1result.textContent = '';
        }
        telephone1result.style.color = 'red';
        telephone1result.style.display = 'block';
        return false;
    }
    return true;

}
$(document).on('keyup', '#Telephone1', _ValidateTelephone1);
//document.getElementById("Telephone1").addEventListener('keyup', _ValidateTelephone1);

//telephonmet2 validation
const ValidateTelephone2 = (telephone2) => {
    return telephone2.match(/^[0-9]{10}$/);
}


const _ValidateTelephone2 = () => {
    const telephone2result = document.getElementById("Telephone2Result");
    const telephone2reusltvalue = document.getElementById("Telephone2");
    const telephone2 = telephone2reusltvalue.value;

    telephone2result.textContent = '';
    if (!ValidateTelephone2(telephone2)) {
        telephone2result.textContent = telephone2 + 'is invalid';
        if (telephone2.trim() === '') {
            telephone2result.textContent = '';
        }
        telephone2result.style.color = 'red';
        telephone2result.style.display = 'block';
        return false;
    }
    return true;
}
$(document).on('keyup', '#Telephone2', _ValidateTelephone2);
//document.getElementById("Telephone2").addEventListener('keyup', _ValidateTelephone2);



//email account
const validateEmailAccount = (emailaccount) => {
    return emailaccount.match(
        /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/
    )
};

const _validateEmailAccount = () => {

    const emailaccountresult = document.getElementById("EmailAccountsResult");
    const emailaccountresultvalue = document.getElementById("EmailAccounts");
    const emailaccount = emailaccountresultvalue.value;
    emailaccountresult.textContent = '';

    if (!validateEmailAccount(emailaccount)) {
        emailaccountresult.textContent = emailaccount + 'is invalid';
        if (emailaccount.trim() === '') {
            emailaccountresult.textContent = '';
        }
        emailaccountresult.style.color = 'red';
        emailaccountresult.style.display = 'block';
        return false;
    }
    return true;
}
$(document).on('keyup', '#EmailAccounts', _validateEmailAccount);
//document.getElementById("EmailAccounts").addEventListener('keyup', _validateEmailAccount);



//email emergency
const validateEmailEmergency = (emailemergency) => {
    return emailemergency.match(
        /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/
    )
};

const _validateEmailEmergency = () => {

    const emailemergencyresult = document.getElementById("EmailEmergencyResult");
    const emailemergencyresultvalue = document.getElementById("EmailEmergency");
    const emailemergency = emailemergencyresultvalue.value;
    emailemergencyresult.textContent = '';

    if (!validateEmailAccount(emailemergency)) {
        emailemergencyresult.textContent = emailemergency + 'is invalid';
        if (emailemergency.trim() === '') {
            emailemergencyresult.textContent = '';
        }
        emailemergencyresult.style.color = 'red';
        emailemergencyresult.style.display = 'block';
        return false;
    }
    return true;

}
$(document).on('keyup', '#EmailEmergency', _validateEmailEmergency);
//document.getElementById("EmailEmergency").addEventListener('keyup', _validateEmailEmergency);


//accounttype id  validation

//document.getElementById("AccountTypeId").addEventListener('keyup', _AccountTypeId);


//vat code
const ValidateVatNumber = (vatnumber) => {
    return vatnumber.match(/^[0-9]+$/);
}

const _ValidateVatNumber = () => {
    const vatnumberresult = document.getElementById("VatNumberResult");
    const vatnumberresultvalue = document.getElementById("VatNumber");
    const vatnumber = vatnumberresultvalue.value;

    vatnumberresult.textContent = '';
    if (!ValidateVatNumber(vatnumber)) {
        vatnumberresult.textContent = vatnumber + 'is invalid';
        if (vatnumber.trim() === '') {
            vatnumberresult.textContent = '';
        }
        vatnumberresult.style.color = 'red';
        vatnumberresult.style.display = 'block';

        return false;
    }
    return true;
}
$(document).on('keyup', '#VatNumber', _ValidateVatNumber);
//document.getElementById("VatNumber").addEventListener('keyup', _ValidateVatNumber);



//reg no  validation

const validateRegNo = (regno) => {
    return regno.match(/^[0-9]+$/);
};

const _validateRegNo = () => {
    const regnoResult = document.getElementById("RegNumberResult");
    const regnoResultvalue = document.getElementById("RegNumber");
    const regno = regnoResultvalue.value;
    regnoResult.textContent = '';

    if (!validateRegNo(regno)) {
        regnoResult.textContent = regno + 'is invalid';
        if (regno.trim() === '') {
            regnoResult.textContent = '';
        }
        regnoResult.style.color = 'red';
        regnoResult.style.display = 'block';
        return false;
    }
    return true;
}
$(document).on('keyup', '#RegNumber', _validateRegNo);

//document.getElementById("RegNumber").addEventListener('keyup', _validateRegNo);


//creditlimit  validation

const validateCreditLimit = (creditlimit) => {
    return creditlimit.match(/[+-]?([0-9]*[.])?[0-9]+/);
};

const _validateCreditLimit = () => {
    const creditlimitresult = document.getElementById("CreditLimitResult");
    const creditlimitresultvalue = document.getElementById("CreditLimit");
    const creditlimit = creditlimitresultvalue.value;
    creditlimitresult.textContent = '';

    if (!validateCreditLimit(creditlimit)) {
        creditlimitresult.textContent = creditlimit + 'is invalid';
        if (creditlimit.trim() === '') {
            creditlimitresult.textContent = '';
        }
        creditlimitresult.style.color = 'red';
        creditlimitresult.style.display = 'block';
        return false;
    }
    return true;

}
$(document).on('keyup', '#CreditLimit', _validateCreditLimit);
//document.getElementById("CreditLimit").addEventListener('keyup', _validateCreditLimit);



//interest  validation

const validateInterest = (interest) => {
    return interest.match(/[+-]?([0-9]*[.])?[0-9]+/);
};

const _validateInterest = () => {
    const interestresult = document.getElementById("InterestResult");
    const interestresultvalue = document.getElementById("Interest");
    const interest = interestresultvalue.value;

    interestresult.textContent = '';

    if (!validateInterest(interest)) {
        interestresult.textContent = interest + 'is invalid';
        if (interest.trim() === '') {
            interestresult.textContent = '';
        }
        interestresult.style.color = 'red';
        interestresult.style.display = 'block';
        return false;
    }
    return true;


}
$(document).on('keyup', '#Interest', _validateInterest);
//document.getElementById("Interest").addEventListener('keyup', _validateInterest);



//dcbalance  validation

const DcBalanceResult = (dcbalance) => {
    return dcbalance.match(/^[0-9]+(\.[0-9]+)?$/);
};

const _DcBalanceResult = () => {
    const dcbalanceresult = document.getElementById("DcBalanceResult");
    const dcbalanceresultvalue = document.getElementById("DcBalance");
    const dcbalance = dcbalanceresultvalue.value;

    dcbalanceresult.textContent = '';

    if (!DcBalanceResult(dcbalance)) {
        dcbalanceresult.textContent = dcbalance + 'is invalid';
        if (dcbalance.trim() === '') {
            dcbalanceresult.textContent = '';
        }
        dcbalanceresult.style.color = 'red';
        dcbalanceresult.style.display = 'block';
        return false;
    }
    return true;

}
$(document).on('keyup', '#DcBalance', _DcBalanceResult);


const ValidateWebsite = (website) => {
    return website.match(/^https?:\/\/[a-zA-Z0-9.-]+(?:\:[0-9]+)?(?:\/.*)?$/);
}

const _ValidateWebsite = () => {
    const websiteresult = document.getElementById("WebsiteResult");
    const websiteresultvalue = document.getElementById("Website");
    const website = websiteresultvalue.value;

    websiteresult.textContent = '';
    if (!ValidateWebsite(website)) {
        websiteresult.textContent = website + 'is invalid';
        if (website.trim() === '') {
            websiteresult.textContent = '';
        }

        websiteresult.style.color = 'red';
        websiteresult.style.display = 'block';
        return false;
    }
    return true;

}
$(document).on('keyup', '#Website', _ValidateWebsite);

//post only valid

function areAllFieldsValid() {
    var allFieldsValid = true;
    //$("input").each(function () {   
    //    /*   if ($(this).val().trim() !== "") {*/
    //    if ($(this).val() == "" || $(this).val() == null ) {
    //  /* if ($(this).val().trim() === "") {*/
    //        allFieldsValid = false;
    //    }
    //});
    if (validate() == false || validateName() == false || ValidateBuyerCode() == false || ValidateCustomerName() == false || _ValidateAccount() == false || _ValidateAccountName() == false || _ValidateAccountDescription() == false
        || _ValidateSwiftCode() == false  || _ValidateTelephone1() == false || _ValidateTelephone2() == false || _validateEmailAccount() == false || _validateEmailEmergency() == false
        || _ValidateVatNumber() == false || _validateRegNo() == false || _validateCreditLimit() == false || _validateInterest() == false || _DcBalanceResult() == false 
        || _ValidateWebsite() == false) {
        allFieldsValid = false;
    }

    return allFieldsValid;
}

// Disable the save button initially
$("#saveButton").prop("disabled", true);

// Enable the save button only when all fields are valid
$("input").on("input", function () {
    if (areAllFieldsValid()) {
        $("#saveButton").prop("disabled", false);
    } else {
        $("#saveButton").prop("disabled", true);
    }
});




function Save() {
    var BuyerMasterId = $("#BuyerMasterId").val();
    var CustomerName = $("#CustomerName").val();
    var BuyerCode = $("#BuyerCode").val();
    var BuyerShortName = $("#BuyerShortName").val();
    var Account = $("#Account").val();
    var AccountName = $("#AccountName").val();
    var AccountDescription = $("#AccountDescription").val();
    var SwiftCode = $("#SwiftCode").val();
    var Physical1 = $("#Physical1").val();

    var PhysicalCode = $("#PhysicalCode").val();

    var CurrencyId = $("#CurrencyId").val();
    var Telephone1 = $("#Telephone1").val();
    var Telephone2 = $("#Telephone2").val();
    var EmailContact = $("#EmailContact").val();
    var EmailAccounts = $("#EmailAccounts").val();
    var EmailEmergency = $("#EmailEmergency").val();
    var AccountTypeId = $("#AccountTypeId").val();
    var VatNumber = $("#VatNumber").val();
    var RegNumber = $("#RegNumber").val();

    var CreditLimit = $("#CreditLimit").val();
    var ChargeInterest = $("#ChargeInterest").val();
    var Interest = $("#Interest").val();
    var DateAdded = $("#DateAdded").val();
    var TaxTypeId = $("#TaxTypeId").val();
    var ForeignCurrency = $("#ForeignCurrency").val();
    var DcBalance = $("#DcBalance").val();
    var ForeignDcBalance = $("#ForeignDcBalance").val();
    var Website = $("#Website").val();


    var formdata = new FormData();
    formdata.append("BuyerMasterId", BuyerMasterId);
    formdata.append("CustomerName", CustomerName);
    formdata.append("BuyerCode", BuyerCode);
    formdata.append("BuyerShortName", BuyerShortName);
    formdata.append("Account", Account);
    formdata.append("AccountName", AccountName);
    formdata.append("AccountDescription", AccountDescription);
    formdata.append("SwiftCode", SwiftCode);
    formdata.append("Physical1", Physical1);

    formdata.append("PhysicalCode", PhysicalCode);

    formdata.append("CurrencyId", CurrencyId);
    formdata.append("Telephone1", Telephone1);
    formdata.append("Telephone2", Telephone2);
    formdata.append("EmailContact", EmailContact);
    formdata.append("EmailAccounts", EmailAccounts);
    formdata.append("EmailEmergency", EmailEmergency);
    formdata.append("AccountTypeId", AccountTypeId);
    formdata.append("VatNumber", VatNumber);
    formdata.append("RegNumber", RegNumber);

    formdata.append("CreditLimit", CreditLimit);
    formdata.append("ChargeInterest", ChargeInterest);
    formdata.append("Interest", Interest);
    formdata.append("DateAdded", DateAdded);
    formdata.append("TaxTypeId", TaxTypeId);
    formdata.append("ForeignCurrency", ForeignCurrency);
    formdata.append("DcBalance", DcBalance);
    formdata.append("ForeignDcBalance", ForeignDcBalance);
    formdata.append("Website", Website);



    if (CustomerName != "" && BuyerCode != "" && EmailContact != "" && BuyerShortName != "" && Physical1 != "" && PhysicalCode != "" && Telephone1 != "" && EmailEmergency != "" &&
        EmailAccounts != "" && AccountTypeId != "" && VatNumber != "" && RegNumber != "" && CreditLimit != "" && Interest != "" && TaxTypeId != "" && ForeignCurrency != "" && DcBalance != "" && Website != "") {

        //for (var [key, value] of formdata.entries()) {
        //    console.log(`${key}: ${value}`);
        //}

        if (areAllFieldsValid() == true) {

            if (BuyerMasterId == "0") {
                $.ajax({
                    type: 'POST',
                    url: '/BuyerMater/BuyerModel',
                    contentType: false,
                    processData: false,
                    dataType: 'html',
                    data: formdata,
                    success: function (data) {
                        data = JSON.parse(data);
                        console.log(data);

                        if (data.success == true) {
                            alert('Saved Successfully.');
                            window.location.href = "/BuyerMater/BuyerMater";
                            return false;
                        }
                        else if (data.message == "already exist") {
                            alert('Buyer already exists in the database.');
                            window.location.href = "/BuyerMater/BuyerMasteDetails";
                            return false;
                        }
                        else if (data.success == false) {
                            alert('Save process Failed.');
                            window.location.href = "/BuyerMater/BuyerMasteDetails";
                            return false;

                        }

                    },
                    error: function (ex) {
                        alert('Already Exist in the database.');
                    }

                });
            }
            else {
                $.ajax({
                    type: 'POST',
                    url: '/BuyerMater/Update',
                    contentType: false,
                    processData: false,
                    dataType: 'html',  // due to thi we can parse the json object 
                    data: formdata,
                    success: function (data) {

                        console.log(data);
                        data = JSON.parse(data);
                        console.log("json :", data);
                        if (data.AlertMessage == "Updated") {
                            alert('updates Successfully.');
                            window.location.href = "/BuyerMater/BuyerMater";
                            return false;
                        }
                        else if (data.values == "already exist") {
                            alert('Buyer already exists in the database.');
                            window.location.href = "/BuyerMater/BuyerMater";
                            return false;
                        }
                        else {
                            alert('updates process Failed.');
                            window.location.href = "/BuyerMater/BuyerMasteDetails";
                            return false;
                        }
                    }
                });
            }
        }
        else {
            alert('Please fill in all required fields.');
            return false;
        }
    }

    else {
        var errorFields = [];
        ///validation fore all fields
        if (CustomerName == "") {
            $("#CustomerNameResult").text("Please enter Customer Name");
            $("#CustomerNameResult").show();
            errorFields.push("CustomerName");
        }

        $("#CustomerName").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#CustomerNameResult").hide();
                ValidateCustomerName();
            } else {
                $("#CustomerNameResult").text("Please enter Customer Name");
                $("#CustomerNameResult").show();
            }
        });

        if (BuyerCode == "") {
            $("#BuyerCodeResult").text("Please enter Buyer Code");
            $("#BuyerCodeResult").show();
            errorFields.push("BuyerCode");
        }

        $("#BuyerCode").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#BuyerCodeResult").hide();
                ValidateBuyerCode();
            } else {
                $("#BuyerCodeResult").text("Please enter Buyer Name");
                $("#BuyerCodeResult").show();
            }
        });


        if (BuyerShortName == "") {
            $("#NameResult").text("Please enter Account");
            $("#NameResult").show();
            errorFields.push("BuyerShortName");
        }
        $("#BuyerShortName").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#NameResult").hide();
                validateName();

            } else {
                $("#NameResult").text("Please enter Account");
                $("#NameResult").show();
            }
        });



        if (EmailContact == "") {
            $("#EmailResult").text("Please enter EmailContact");
            $("#EmailResult").show();
            errorFields.push("EmailContact");
        }
        $("#EmailContact").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#EmailResult").hide();
                //var a = $("#EmailContact").val();
                validate();
            } else {
                $("#EmailResult").text("Please enter EmailContact");
                $("#EmailResult").show();
            }
        });

        if (Account == "") {
            $("#AccountResult").text("Please enter Account");
            $("#AccountResult").show();
            errorFields.push("Account");
        }
        $("#Account").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#AccountResult").hide();
                _ValidateAccount();
            } else {
                $("#AccountResult").text("Please enter Account");
                $("#AccountResult").show();
            }
        });


        if (AccountName == "") {
            $("#AccountNameResult").text("Please enter AccountName");
            $("#AccountNameResult").show();
            errorFields.push("AccountName");
        }
        $("#AccountName").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#AccountNameResult").hide();
                _ValidateAccountName();
            } else {
                $("#AccountNameResult").text("Please enter AccountName");
                $("#AccountNameResult").show();
            }
        });

        if (AccountDescription == "") {
            $("#AccountDescriptionResult").text("Please enter AccountDescription");
            $("#AccountDescriptionResult").show();
            errorFields.push("AccountDescription");
        }
        $("#AccountDescription").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#AccountDescriptionResult").hide();
                _ValidateAccountDescription();
            } else {
                $("#AccountDescriptionResult").text("Please enter Account description");
                $("#AccountDescriptionResult").show();
            }
        });

        if (SwiftCode == "") {
            $("#SwiftCodeResult").text("Please enter SwiftCode");
            $("#SwiftCodeResult").show();
            errorFields.push("SwiftCode");
        }
        $("#SwiftCode").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#SwiftCodeResult").hide();
                _ValidateSwiftCode();
            } else {
                $("#SwiftCodeResult").text("Please enter swift code");
                $("#SwiftCodeResult").show();
            }
        });








        if (PhysicalCode == "") {
            $("#PhysicalCodeResult").text("Please enter PhysicalCode");
            $("#PhysicalCodeResult").show();
            errorFields.push("PhysicalCode");
        }
        $("#PhysicalCode").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#PhysicalCodeResult").hide();
                _ValidatePhysicalCode();
            } else {
                $("#PhysicalCodeResult").text("Please enter PhysicalCode");
                $("#PhysicalCodeResult").show();
            }
        });





        if ($("#CurrencyId").val() == "") {
            $("#CurrencyIdResult").text("Please select CurrencyId");
            $("#CurrencyIdResult").show();
            errorFields.push("CurrencyId");
        }
        $("#CurrencyId").on("change", function () {
            if ($(this).val() != "") {
                $("#CurrencyIdResult").hide();
            } else {
                $("#CurrencyIdResult").text("Please select CurrencyId");
                $("#CurrencyIdResult").show();
            }
        });


        if (Telephone1 == "") {
            $("#Telephone1Result").text("Please enter Telephone1");
            $("#Telephone1Result").show();
            errorFields.push("Telephone1");
        }
        $("#Telephone1").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#Telephone1Result").hide();
                _ValidateTelephone1();
            } else {
                $("#Telephone1Result").text("Please enter Telephone1");
                $("#Telephone1Result").show();
            }
        });

        if (Telephone2 == "") {
            $("#Telephone2Result").text("Please enter Telephone2");
            $("#Telephone2Result").show();
            errorFields.push("Telephone2");
        }
        $("#Telephone2").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#Telephone2Result").hide();
                _ValidateTelephone2();
            } else {
                $("#Telephone2Result").text("Please enter Telephone2");
                $("#Telephone2Result").show();
            }
        });



        if (EmailEmergency == "") {
            $("#EmailEmergencyResult").text("Please enter EmailEmergency");
            $("#EmailEmergencyResult").show();
            errorFields.push("EmailEmergency");
        }
        $("#EmailEmergency").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#EmailEmergencyResult").hide();
                _validateEmailEmergency();
            } else {
                $("#EmailEmergencyResult").text("Please enter EmailEmergency");
                $("#EmailEmergencyResult").show();
            }
        });

        if (EmailAccounts == "") {
            $("#EmailAccountsResult").text("Please enter EmailAccounts");
            $("#EmailAccountsResult").show();
            errorFields.push("EmailAccounts");
        }
        $("#EmailAccounts").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#EmailAccountsResult").hide();
                _validateEmailAccount();
            } else {
                $("#EmailAccountsResult").text("Please enter Emailemergency");
                $("#EmailAccountsResult").show();
            }
        });

        if (VatNumber == "") {
            $("#VatNumberResult").text("Please enter VatNumber");
            $("#VatNumberResult").show();
            errorFields.push("VatNumber");
        }
        $("#VatNumber").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#VatNumberResult").hide();
                _ValidateVatNumber();
            } else {
                $("#VatNumberResult").text("Please enter VatNumber");
                $("#VatNumberResult").show();
            }
        });

        if (RegNumber == "") {
            $("#RegNumberResult").text("Please enter RegNumber");
            $("#RegNumberResult").show();
            errorFields.push("RegNumber");
        }
        $("#RegNumber").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#RegNumberResult").hide();
                _validateRegNo();
            } else {
                $("#RegNumberResult").text("Please enter RegNumber");
                $("#RegNumberResult").show();
            }
        });


        if (CreditLimit == "") {
            $("#CreditLimitResult").text("Please enter CreditLimit");
            $("#CreditLimitResult").show();
            errorFields.push("CreditLimit");
        }
        $("#CreditLimit").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#CreditLimitResult").hide();
                _validateCreditLimit();
            } else {
                $("#CreditLimitResult").text("Please enter CreditLimit");
                $("#CreditLimitResult").show();
            }
        });


        if (Interest == "") {
            $("#InterestResult").text("Please enter Interest");
            $("#InterestResult").show();
            errorFields.push("Interest");
        }
        $("#Interest").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#InterestResult").hide();
                _validateInterest();
            } else {
                $("#InterestResult").text("Please enter Interest");
                $("#InterestResult").show();
            }
        });



        if ($("#TaxTypeId").val() == "") {
            $("#TaxTypeIdResult").text("Please select TaxType");
            $("#TaxTypeIdResult").show();
            errorFields.push("TaxTypeId");
        }

        $("#TaxTypeId").on("change", function () {
            if ($(this).val() != "") {
                $("#TaxTypeIdResult").hide();
            } else {
                $("#TaxTypeIdResult").text("Please select TaxType");
                $("#TaxTypeIdResult").show();
            }
        });
        if ($("#AccountTypeId").val() == "") {
            $("#AccountTypeIdResult").text("Please select AccountType");
            $("#AccountTypeIdResult").show();
            errorFields.push("AccountTypeId");
        }

        $("#AccountTypeId").on("change", function () {
            if ($(this).val() != "") {
                $("#AccountTypeIdResult").hide();
            } else {
                $("#AccountTypeIdResult").text("Please select AccountType");
                $("#AccountTypeIdResult").show();
            }
        });

        if ($("#ForeignCurrency").val() == "") {
            $("#ForeignCurrencyResult").text("Please select ForeignCurrency");
            $("#ForeignCurrencyResult").show();
            errorFields.push("ForeignCurrency");
        }
        $("#ForeignCurrency").on("change", function () {
            if ($(this).val() != "") {
                $("#ForeignCurrencyResult").hide();
            } else {
                $("#ForeignCurrencyResult").text("Please select ForeignCurrency");
                $("#ForeignCurrencyResult").show();
            }
        });

        if (DcBalance == "") {
            $("#DcBalanceResult").text("Please enter DcBalance");
            $("#DcBalanceResult").show();
            errorFields.push("DcBalance");
        }
        $("#DcBalance").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#DcBalanceResult").hide();
                _DcBalanceResult();
            } else {
                $("#DcBalanceResult").text("Please enter DcBalance");
                $("#DcBalanceResult").show();
            }
        });

        //if (ForeignDcBalance == "") {
        //    $("#ForeignDcBalanceResult").text("Please enter ForeignDcBalance");
        //    $("#ForeignDcBalanceResult").show();
        //    errorFields.push("ForeignDcBalance");
        //}
        //$("#ForeignDcBalance").on("keyup blur", function () {
        //    if ($(this).val() != "") {
        //        $("#ForeignDcBalanceResult").hide();
        //        _ForeignDcBalanceResult();
        //    } else {
        //        $("#ForeignDcBalanceResult").text("Please enter ForeignDcBalance");
        //        $("#ForeignDcBalanceResult").show();
        //    }
        //});

        if (Website == "") {
            $("#WebsiteResult").text("Please enter Website");
            $("#WebsiteResult").show();
            errorFields.push("ForeignDcBalance");
        }
        $("#Website").on("keyup blur", function () {
            if ($(this).val() != "") {
                $("#WebsiteResult").hide();
                // _ForeignDcBalanceResult();
            } else {
                $("#WebsiteResult").text("Please enter ForeignDcBalance");
                $("#WebsiteResult").show();
            }
        });


    }
}



// category



function categoryfun() {
    var City = $('#CategoryName').val();
    var phoneregx = /^[a-zA-Z\s\.]*$/;
    if (City === "" || City === null) {
        $('#error31').text("Please Enter CategoryName Name");
    } else if (phoneregx.test(City)) {
        $('#error31').text("");
    } else {
        $('#error31').text("Please enter valid Name");
    }
}

function categoryCod() {
    var City2 = $('#CategoryCode').val();
    if (City2 === "" || City2 === null) {
        $('#error').text("Please Enter Category Code");
    } else {
        $('#error').text("");
    }
}

function categorytypeCod() {
    var City2 = $('#Categorytype').val();
    if (City2 === "" || City2 === null) {
        $('#error3').text("Please Select Category Type");
    } else {
        $('#error3').text("");
    }
}

function category() {
    categorytypeCod();
    categoryCod();
    categoryfun();
}
