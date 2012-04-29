using System.Linq;
using System.Web.Mvc;
using Gamification.Core.DataAccess;
using Gamification.Core.Entities.Constraints;
using Gamification.Web.Areas.ProjectsControlPanel.ViewModels;

namespace Gamification.Web.Areas.ProjectsControlPanel.Controllers
{
    public class ConstraintsController : Controller
    {
        private readonly IRepository<BaseNumericBasedConstraint> numericConstraintsRepository;
        private readonly IRepository<BaseStringCollectionConstraint> collectionConstraintsRepository;

        public ConstraintsController(
            IRepository<BaseNumericBasedConstraint> numericConstraintsRepository,
            IRepository<BaseStringCollectionConstraint> collectionConstraintsRepository)
        {
            this.numericConstraintsRepository = numericConstraintsRepository;
            this.collectionConstraintsRepository = collectionConstraintsRepository;
        }

        public ActionResult Index(int projectId)
        {
            var viewModel = new ConstraintsIndexViewModel();
            viewModel.NumericConstraints = this.numericConstraintsRepository.Where(x => x.Project.Id == projectId).ToList();
            viewModel.StringsConstraits = this.collectionConstraintsRepository.Where(x => x.Project.Id == projectId).ToList();
            
            return View(viewModel);
        }

        public ActionResult Show(int id, BaseConstraintsTypes type)
        {
            if (type == BaseConstraintsTypes.BaseNumericConstrait)
            {
                var constraint = this.numericConstraintsRepository.GetById(id);
                return View(constraint);
            }
            else
            {
                return View();
            }
        }

        //
        // GET: /ProjectsControlPanel/Constraints/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ProjectsControlPanel/Constraints/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        
        //
        // GET: /ProjectsControlPanel/Constraints/Edit/5
 
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /ProjectsControlPanel/Constraints/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /ProjectsControlPanel/Constraints/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /ProjectsControlPanel/Constraints/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
