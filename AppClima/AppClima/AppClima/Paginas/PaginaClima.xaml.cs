using AppClima.Clases;
using AppClima.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppClima.Paginas
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PaginaClima : ContentPage
	{
		public PaginaClima ()
		{
			InitializeComponent ();
            this.BindingContext = new Clima();
		}

        public async void BuscarClima_Clicked(object sender,EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCiudad.Text))
            {
                var clima = await ServicioClima.ConsultarClima(txtCiudad.Text);
                if (clima != null)
                {
                    this.BindingContext = clima;
                    btnBuscar.Text = "Buscar de nuevo";
                }
            }
        }

    }
}