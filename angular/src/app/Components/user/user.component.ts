import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/Services/user.service';
import { Title } from '@angular/platform-browser';
import { ToastrService } from 'ngx-toastr';
import { Contact } from 'src/app/Models/Contact';
import { User } from 'src/app/Models/user';
import { NgForm } from '@angular/forms';

@Component({
selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
displayedColumns: string[] = ['firstName','lastName','phone', 'email', 'Operations'];
  
  users: User[] = [];
  contact = new Contact(0,0,"");
 user = new User(0,"","", this.contact);
  flag: boolean;
  constructor(private titleService: Title,private toastr: ToastrService, private UserService : UserService) {
    this.titleService.setTitle("UserAdmin");
  }
  ngOnInit() {
    this.GetAll();
  }


  GetAll() {
    this.UserService.GetAll().subscribe(a => {
      this.users = a;
    }
      , err => console.log(err));
  }
  Delete(id: number, idx) {
    // let confirmed = confirm("Are you sure!");
    // console.log(x);
    // let index = this.users.findIndex(s => s.ID === id);
    let index = this.users.indexOf(idx);

    this.UserService.Delete(id).subscribe(a => {
      this.users.splice(index, 1);
      this.toastr.success('User Deleted!', 'Done!');
    }, (err) => {
      this.toastr.error('Couldn\'t delete the User', 'Failed.');
    });
  }
  Details(id: number) {
    this.flag = true;
    this.UserService.GetById(id).subscribe(a => {
      this.user = a;
    this.contact = a.contact;
    });
  }
  Create() {
    this.UserService.Create(this.user).subscribe((a) => {
      this.users.push(a);
      this.toastr.success('New user created!', 'Done!');
    }, (err) => {
      this.toastr.error('Couldn\'t create new user', 'Failed.');
    });
  }
  Update() {

    this.UserService.Update(this.user.id, this.user).subscribe((a) => {

      let index = this.users.findIndex(s => s.id === this.user.id);
      this.users.splice(index, 1, this.user);
      this.toastr.success('the user has been updated!', 'Done!');
    }, (err) => {
      this.toastr.error('Couldn\'t update the user', 'Failed.');
    });
  }

  Reset(form : NgForm){
    
    form.reset();
    this.flag = false;
    window.location.reload();
  }

}
