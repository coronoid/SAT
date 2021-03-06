using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SAT.DATA.EF;

namespace SAT.UI.MVC.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private SATEntities db = new SATEntities();

        // GET: Students
        [Authorize(Roles = "Admin,Scheduling")]
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.StudentStatus);
            return View(students.ToList());
        }

        // GET: Students/Tile
        public ActionResult Tile()
        {
            var students = db.Students.Include(s => s.StudentStatus);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        [Authorize(Roles = "Admin,Scheduling")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase PhotoUrl)
        {
            if (ModelState.IsValid)
            {

                #region File Upload

                string imageName = "noImage.png";

                if (PhotoUrl != null)
                {
                    imageName = PhotoUrl.FileName;

                    string ext = imageName.Substring(imageName.LastIndexOf("."));

                    string[] goodExts = new string[] { ".jpg", ".png", ".jpeg", ".gif" };

                    if (goodExts.Contains(ext.ToLower()))
                    {
                        imageName = Guid.NewGuid() + ext;

                        PhotoUrl.SaveAs(Server.MapPath("~/Content/assets/img/Student/" + imageName));

                    }
                    else
                    {
                        imageName = "noImage.png";
                    }


                }

                student.PhotoUrl = imageName;

                #endregion
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase PhotoUrl)
        {
            if (ModelState.IsValid)
            {
                #region File Upload

                if (PhotoUrl != null)
                {
                    string imageName = PhotoUrl.FileName;

                    string ext = imageName.Substring(imageName.LastIndexOf("."));

                    string[] goodExts = new string[] { ".jpg", ".png", ".jpeg", ".gif" };

                    if (goodExts.Contains(ext.ToLower()))
                    {

                        imageName = Guid.NewGuid() + ext;

                        PhotoUrl.SaveAs(Server.MapPath("~/Content/assets/img/Student/" + imageName));

                        if (student.PhotoUrl != "noImage.png" && student.PhotoUrl != null)
                        {
                            System.IO.File.Delete(Server.MapPath("~/Content/assets/img/Student/" + student.PhotoUrl));
                        }

                    }

                    student.PhotoUrl = imageName;

                }


                #endregion


                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
