
/*----------------- Public key de mercado pago -----------------*/
const mp = new MercadoPago('TEST-795216f6-3c7a-4bae-b0cd-6b36a85a2614', {
    locale: 'es-MX'
});


/*----------------- Evento al precionar el boton de compra -----------------*/
document.getElementById('btnCompra').addEventListener("click", async () => {

    try {
        /*- Creacion de la orden de compra -*/
        const Orden = {
            Titulo: document.querySelector(".titulo").innerText,
            Cantidad: 1,
            Precio: 150,
        };

        /*- Creacion de la preferencia -*/
        const Respuesta = await fetch("/Home/CrearPreferencia", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(Orden),
        });

        const Preferencia = await Respuesta.json();
        CrearBotonMP(Preferencia.id);
    } catch (ex) {
        alert("Error")
    }
});

/*----------------- Renderizacion del boton de mercado pago -----------------*/
const CrearBotonMP = (PreferenciaID) => {
    const bricksBuilder = mp.bricks();

    const Renderizacion = async () => {
        if (window.checkoutButton) window.checkoutButton.unmount(); 
        await bricksBuilder.create("wallet", "wallet_container", {
            initialization: {
                preferenceId: PreferenciaID,
            },
        });
    };
    Renderizacion();
}




