using System.Collections.Generic;
using VMToHackASM.Models;
using VMToHackASM.Utilities;

namespace VMToHackASM.Factories
{
    public static class VmInstructionFactory
    {
        public static IEnumerable<IVmInstruction> CreateCollection(IEnumerable<string[]> vmOperations)
        {
            var vmOperationInstances = new List<IVmInstruction>();

            foreach (var vmOperation in vmOperations)
            {
                string vmInstructionType = vmOperation[0];
                IVmInstruction vmOperationInstance;

                if (vmOperation.Length > 1)
                {
                    var operationType = EnumUtils.StringToEnum<VmOperationType>(vmInstructionType);
                    string segmentString = vmOperation[1];
                    short value = short.Parse(vmOperation[2]);
                    var segment = EnumUtils.StringToEnum<VmSegment>(segmentString);

                    vmOperationInstance = new VmOperation(operationType, segment, value);
                }
                else
                {
                    var commandType = EnumUtils.StringToEnum<VmCommandType>(vmInstructionType);
                    vmOperationInstance = new VmCommand(commandType);
                }

                vmOperationInstances.Add(vmOperationInstance);
            }

            return vmOperationInstances;
        }
    }
}