import {Injectable} from '@angular/core';
import Swal, {SweetAlertIcon} from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AlertService {

  constructor() {
  }

  error(message: string, title: string): void {
    this.showAlert(title, message, 'error');
  }

  permissionError(): void {
    Swal.fire({
      icon: 'error',
      position : 'top',
      title: 'Oops...',
      text: 'Não tem permissão para fazer isso!',
      footer: '<a href="">Contactar responsável</a>'
    })
  }

  operationsSucced(message: string) {
    Swal.fire({
      position: 'top',
      icon: 'success',
      title: message,
      showConfirmButton: false,
      timer: 2500
    })
  }

  info(message: string, title: string): void {
    this.showAlert(title, message, 'info');
  }

  private showAlert(title: string, message: string, icon: SweetAlertIcon): void {
    Swal.fire(title, message, icon)
  }


}
