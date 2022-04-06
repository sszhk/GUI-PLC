using System.Runtime.InteropServices;
class cpu_load
{
  [DllImport("cpu_load.dll")]
  static extern ushort get_cpu_load();
}
