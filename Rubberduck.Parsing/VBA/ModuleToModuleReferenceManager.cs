﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rubberduck.VBEditor;

namespace Rubberduck.Parsing.VBA
{
    public class ModuleToModuleReferenceManager: ModuleToModuleReferenceManagerBase 
    {
        private readonly RubberduckParserState _state;

        public ModuleToModuleReferenceManager(RubberduckParserState state)
        {
            _state = state;
        }


        public override void AddModuleToModuleReference(QualifiedModuleName referencingModule, QualifiedModuleName referencedModule)
        {
            _state.AddModuleToModuleReference(referencingModule, referencedModule);
        }

        public override void RemoveModuleToModuleReference(QualifiedModuleName referencedModule, QualifiedModuleName referencingModule)
        {
            _state.RemoveModuleToModuleReference(referencedModule, referencingModule);
        }

        public override void ClearModuleToModuleReferencesFromModule(QualifiedModuleName referencingModule)
        {
            _state.ClearModuleToModuleReferencesFromModule(referencingModule);
        }

        public override void ClearModuleToModuleReferencesToModule(QualifiedModuleName referencedModule)
        {
            _state.ClearModuleToModuleReferencesToModule(referencedModule);
        }

        public override ICollection<QualifiedModuleName> ModulesReferencedBy(QualifiedModuleName referencingModule)
        {
            return _state.ModulesReferencedBy(referencingModule);
        }

        public override ICollection<QualifiedModuleName> ModulesReferencing(QualifiedModuleName referencedModule)
        {
            return _state.ModulesReferencing(referencedModule);
        }

    }
}
