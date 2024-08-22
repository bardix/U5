let pathVisit = '/api/VisitApi';
let pathRecover = '/api/RecoverApi';
let pathLocker = '/api/LockerApi';
let pathResearch = '/api/ResearchApi'

$('#visiteModal').on('show.bs.modal', function (event) {
    var button = $(event.relatedTarget); 
    var animalId = button.data('animal-id'); 
    var modal = $(this);
    var buttonVisit = $('#buttonVisit') 

    $.ajax({
        url: `${pathVisit}/${animalId}`,
        type: 'GET',
        success: (data) => {
            var visitsTableBody = modal.find('#visitsTableBody');
            visitsTableBody.empty();
            console.log(data)

            $(data).each((_, visit) => {
                visitsTableBody.append(
                    '<tr>' +
                    '<td>' + visit.dateVisit + '</td>' +
                    '<td>' + visit.typeOfExam + '</td>' +
                    '<td>' + visit.typeOfCure + '</td>' +
                    '</tr>'
                );
            });
            buttonVisit.empty();
            buttonVisit.append(`<a class="btn btn-primary" href=/Visit/Create/${animalId}> Registra una nuova visita </a>`)
        },

        error: function () {
            alert('Errore nel recupero delle visite.');
        }
    });
});



$("#researchM").on('click', () => {
    let microchip = $("#microchipValue").val();

    $.ajax({
        url: `${pathRecover}/${microchip}`,
        method: 'GET',
        success: (data) => {
            console.log(data)
            let dataDiv = $("#information")
            dataDiv.empty();
            dataDiv.append(`
            <div class="card mb-3" style="max-width: 540px;">
                <div class="row g-0">
                    <div class="col-md-4">
                        <img src="${data.image}" class="img-fluid rounded-start" alt="Animal Image"">
                    </div>
                            <div class="col-md-8">
                            <div class="card-body">
                             <h5 class="card-title">${data.animal.name}</h5>
                                <p class="card-text"><strong>Data di registrazione:</strong> ${new Date(data.animal.dateRegister).toLocaleDateString()}</p>
                                     <p class="card-text"><strong>Tipo:</strong> ${data.animal.type}</p>
                            <p class="card-text"><strong>Data di recupero:</strong> ${new Date(data.dateRecover).toLocaleDateString()}</p>
                        <p class="card-text">
                            <strong>Stato:</strong> 
                        <span class="badge ${data.isActive ? 'bg-success' : 'bg-danger'}">
                            ${data.isActive ? 'Attivo' : 'Non Attivo'}
                            </span>
                        </p>
                    </div>
                    </div>
                    </div>
                </div>
            `)
        }
    })
})

$("#ResearchFC").on('click', () => {
    let fiscalCode = $("#fiscalCode").val()
    $.ajax({
        url: `${pathResearch}/${fiscalCode}`,
        method: 'GET',
        success: (data) => {
            console.log(data)
            let fiscalCodeInformation = $("#fiscalCodeInformation");
            fiscalCodeInformation.empty();
            $(data).each((_, inf) => {
                fiscalCodeInformation.append(`
                     <div class="card mb-3">
                        <div class="card-body">
                        <h5 class="card-title">Informazioni Acquirente</h5>
                            <p class="card-text"><strong>Nome:</strong> ${inf.owner.firstName} ${inf.owner.lastName}</p>
                            <p class="card-text"><strong>Prodotto Acquistato:</strong> ${inf.product.name}</p>
                            <p class="card-text"><strong>Data di Vendita:</strong> ${inf.dateSell}</p>
                         </div>
                    </div>
                `);
            })
        }
    })
})

$("#ResearchD").on('click', () => {
    let date = $("#date").val()

    $.ajax({
        url: `${pathResearch}/SellByDate/${date}`,
        method: 'GET',
        success: (data) => {
            console.log(data)
            let dateInformation = $("#dateInformation");
            dateInformation.empty();
            $(data).each((_, inf) => {
                dateInformation.append(`
                                <div class="card mb-3">
                                    <div class="card-body">
                                        <h5 class="card-title">Informazioni Acquirente</h5>
                                            <p class="card-text"><strong>Prodotto Acquistato:</strong> ${inf.product.name}</p>
                                            <p class="card-text"><strong>Data di Vendita:</strong> ${inf.dateSell}</p>
                                    </div>
                                </div>
                            `);
;
            })
        }
    })
})
$(document).on('click','.ReasearchP', function() {
    let product = $(this).data('product-id')
    console.log(product)
    $.ajax({
        url: `${pathResearch}/SearchProduct/${product}`,
        method: 'GET',
        success: (data) => {
            console.log(data)
            let productInf = $("#productInf")

            productInf.empty();
            let drawerInfo;
            if (data.drawer.length == 0) {

                drawerInfo = `<span class="text-danger">non si trova da nessuna parte.</span>`;
            } else {
                drawerInfo = data.drawer.map(draw =>
                    `<span class="shadow-sm mb-1">Cassetto n°: ${draw.drawer.id} nell'armadietto numero ${draw.drawer.locker.numberLocker}</span>`
            ).join(', ');
            }

            let content = `
                <div class="alert alert-success">
                <h4 class="alert-heading">Informazioni sul Prodotto</h4>
                <p><strong>Il prodotto:</strong> ${data.name}</p>
                <p><strong>Posizione:</strong>${drawerInfo}</p>
                </div>
            `;

            productInf.html(content);
        }
    })
})

$(() => {
    $.ajax({
        url: `${pathRecover}`,
        method: 'GET',
        success: (data) => {
            let infAnimal = $('#infAnimal');
            $(data).each((_, inf) => {
                infAnimal.append(`
               <tr>
    <td>
        <span class="d-block text-center">${new Date(inf.dateRecover).toLocaleDateString()}</span>
    </td>
    <td class="text-center">
        <img src="${inf.image}" alt="Animal Image" class="img-thumbnail heightImg">
    </td>
    <td class="text-center">
         <span class="badge ${inf.isActive ? 'bg-success' : 'bg-danger'}">
            ${inf.isActive ? 'Attivo' : 'Non Attivo'}
        </span>
    </td>
    <td class="text-center">
        <a class='btn btn-primary btn-sm' href="#">Chiama la clinica se questo è il tuo animale</a>
    </td>
</tr>
            `)
            })
            
        }
    })
});