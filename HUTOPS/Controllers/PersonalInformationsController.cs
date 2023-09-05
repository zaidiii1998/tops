using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HUTOPS;

namespace HUTOPS.Controllers
{
    public class PersonalInformationsController : Controller
    {
        private HU_TOPSEntities db = new HU_TOPSEntities();

        // GET: PersonalInformations
        public ActionResult Index()
        {
            return View(db.PersonalInformations.ToList());
        }

        // GET: PersonalInformations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalInformation personalInformation = db.PersonalInformations.Find(id);
            if (personalInformation == null)
            {
                return HttpNotFound();
            }
            return View(personalInformation);
        }

        // GET: PersonalInformations/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PersonalInformations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,MiddleName,LastName,CellPhoneNumber,CNIC,EmailAddress,Password")] PersonalInformation personalInformation)
        {
            if (ModelState.IsValid)
            {
                if (personalInformation.FirstName != null && (personalInformation.FirstName.Length < 3 || personalInformation.FirstName.Length > 25))
                {
                    ModelState.AddModelError("FirstName", "Please enter first name length is greater than 3 and less than 25 characters");
                    return View();
                }
                else
                {
                    ModelState.AddModelError("FirstName", "Please enter first name");
                    return View();

                }

                db.PersonalInformations.Add(personalInformation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(personalInformation);
        }

        // GET: PersonalInformations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalInformation personalInformation = db.PersonalInformations.Find(id);
            if (personalInformation == null)
            {
                return HttpNotFound();
            }
            return View(personalInformation);
        }

        // POST: PersonalInformations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,HUTopId,AppId,FirstName,MiddleName,LastName,FatherFirstName,FatherMiddleName,FatherLastName,Gender,HusbandName,DateOfBirth,CountyAdd,CellPhoneNumber,AlternateCellPhoneNumber,TellUs,ResidentialAddress,ResidentialCity,ResidentialCityOther,ResidentialProvince,ResidentialCountry,ResidentialPostalCode,PermanentAddress,PermanentCity,PermanentCityOther,PermanentProvince,PermanentCountry,PermanentPostalCode,CNIC,EmailAddress,AlterEmailAddress,Password,SaveDate,UpdateDate,AppStatus,StudentStatus,TempId,School,SchoolCode,SchoolName,SchoolNameOther,CurrentQualification,CurrentQualificationOther,AlternateLandline,UserId,TestDate,SubmissionDate,Retake,IsAppliedBefore,AppliedBeforeMonth,AppliedBeforeYear,HomePhoneNumber,WhatsAppNumber,GuardianCellPhoneNumber,GuardianEmailAddress,HearAboutHU,HearAboutHUOther,CreatedDatetime,Declaration,IsCompleted")] PersonalInformation personalInformation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(personalInformation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(personalInformation);
        }

        // GET: PersonalInformations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonalInformation personalInformation = db.PersonalInformations.Find(id);
            if (personalInformation == null)
            {
                return HttpNotFound();
            }
            return View(personalInformation);
        }

        // POST: PersonalInformations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonalInformation personalInformation = db.PersonalInformations.Find(id);
            db.PersonalInformations.Remove(personalInformation);
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
