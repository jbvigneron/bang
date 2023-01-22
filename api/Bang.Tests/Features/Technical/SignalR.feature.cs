﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Bang.Tests.Features.Technical
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class CommunicationsEntreLesJoueursAvecSignalRFeature : object, Xunit.IClassFixture<CommunicationsEntreLesJoueursAvecSignalRFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "SignalR.feature"
#line hidden
        
        public CommunicationsEntreLesJoueursAvecSignalRFeature(CommunicationsEntreLesJoueursAvecSignalRFeature.FixtureData fixtureData, Bang_Tests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("fr-FR"), "Features/Technical", "Communications entre les joueurs avec SignalR", null, ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        public virtual void FeatureBackground()
        {
#line 3
#line hidden
            TechTalk.SpecFlow.Table table11 = new TechTalk.SpecFlow.Table(new string[] {
                        "playerName"});
            table11.AddRow(new string[] {
                        "Jean"});
            table11.AddRow(new string[] {
                        "Max"});
            table11.AddRow(new string[] {
                        "Emilie"});
            table11.AddRow(new string[] {
                        "Martin"});
#line 4
 testRunner.Given("ces joueurs souhaitent faire une partie", ((string)(null)), table11, "Sachant que ");
#line hidden
#line 10
 testRunner.When("une partie est initialisée", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quand ");
#line hidden
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Quand une nouvelle partie est créée")]
        [Xunit.TraitAttribute("FeatureTitle", "Communications entre les joueurs avec SignalR")]
        [Xunit.TraitAttribute("Description", "Quand une nouvelle partie est créée")]
        public void QuandUneNouvellePartieEstCreee()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Quand une nouvelle partie est créée", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 12
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 3
this.FeatureBackground();
#line hidden
#line 13
 testRunner.Then("un message \"NewGame\" est envoyé au hub public", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Alors ");
#line hidden
#line 14
 testRunner.And("un message \"GameDeckReady\" est envoyé au hub public", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Et ");
#line hidden
#line 15
 testRunner.And("le jeu contient 80 cartes", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Et ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Quand un joueur rejoint la partie")]
        [Xunit.TraitAttribute("FeatureTitle", "Communications entre les joueurs avec SignalR")]
        [Xunit.TraitAttribute("Description", "Quand un joueur rejoint la partie")]
        public void QuandUnJoueurRejointLaPartie()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Quand un joueur rejoint la partie", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 17
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 3
this.FeatureBackground();
#line hidden
#line 18
 testRunner.When("\"Jean\" rejoint la partie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quand ");
#line hidden
#line 19
 testRunner.And("\"Max\" rejoint la partie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Et ");
#line hidden
#line 20
 testRunner.Then("\"Jean\" reçoit un message de groupe \"PlayerJoin\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Alors ");
#line hidden
#line 21
 testRunner.And("\"Max\" est prêt", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Et ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Quand la partie est prête")]
        [Xunit.TraitAttribute("FeatureTitle", "Communications entre les joueurs avec SignalR")]
        [Xunit.TraitAttribute("Description", "Quand la partie est prête")]
        public void QuandLaPartieEstPrete()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Quand la partie est prête", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 23
this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 3
this.FeatureBackground();
#line hidden
#line 24
 testRunner.When("\"Jean\" rejoint la partie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quand ");
#line hidden
#line 25
 testRunner.And("\"Max\" rejoint la partie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Et ");
#line hidden
#line 26
 testRunner.And("\"Emilie\" rejoint la partie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Et ");
#line hidden
#line 27
 testRunner.And("\"Martin\" rejoint la partie", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Et ");
#line hidden
#line 28
 testRunner.Then("un message \"AllPlayerJoined\" est envoyé au hub public", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Alors ");
#line hidden
#line 29
 testRunner.And("la partie peut commencer", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Et ");
#line hidden
#line 30
 testRunner.And("c\'est au tour du schérif", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Et ");
#line hidden
#line 31
 testRunner.And("un message \"ItsYourTurn\" est envoyé au schérif", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Et ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                CommunicationsEntreLesJoueursAvecSignalRFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                CommunicationsEntreLesJoueursAvecSignalRFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
